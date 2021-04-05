using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Consts;
using Business.Validations.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Concrete.Files;
using Core.Utilities.BusinessRules;
using Core.Utilities.Filing.Database;
using Core.Utilities.Filing.Local;
using Core.Utilities.Guids;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class TissueImageManager : ITissueImageService
    {
        private ITissueImageDal _tissueImageDal;
        private LocalFileSystem _localFileSystem;
        private DatabaseFileSytem _databaseFileSytem;

        public TissueImageManager(ITissueImageDal tissueImageDal)
        {
            _tissueImageDal = tissueImageDal;
            _localFileSystem = new ImageLocalFiling();
            _databaseFileSytem = new ImageDbFiling();
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(TissueImageValidator))]
        public IResult<TissueImage> Add(Image file, int tissueId)
        {
            var result = BusinessRules<TissueImage>.Checker(ImageCountChecker(tissueId));

            TissueImage tissueImage = new TissueImage()
            {
                ImagePath = _localFileSystem.Path,
                Guid = new GuidGenerator().Generator(),
                Date = DateTime.Now,
                TissueId = tissueId,
                Image = _databaseFileSytem.FileToBytes(file)
            };

            if (result != null)
            {
                foreach (var error in result)
                {
                    return new FailResult<TissueImage>(error.Message);
                }
            }

            _localFileSystem.Filing(file, tissueImage.Guid);
            _tissueImageDal.Add(tissueImage);

            return new SuccessResult<TissueImage>(Messages.imageAddSuccess);
        }

        [SecuredOperation("admin")]
        public IResult<TissueImage> Delete(TissueImage tissueImage)
        {
            _tissueImageDal.Delete(tissueImage);
            return new SuccessResult<TissueImage>(Messages.success);
        }

        [SecuredOperation("admin")]
        public IResult<List<TissueImage>> GetImagesPerTissue(int tissueId)
        {          
            var result = BusinessRules<TissueImage>.Checker(IfImageExistCheck(tissueId));
         
            if (result != null)
            {
                if (!result[0].Success)
                {
                    var defaultByteImage = _tissueImageDal.GetAll(p => p.TissueId == 0);
                    return new FailResult<List<TissueImage>>(result[0].Message, defaultByteImage);
                }
            }

            var byteImages = _tissueImageDal.GetAll(p => p.TissueId == tissueId);

            return new SuccessResult<List<TissueImage>>(Messages.success, byteImages);
        }

        [SecuredOperation("admin")]
        public IResult<TissueImage> Update(TissueImage tissueImage)
        {
            tissueImage.Date = DateTime.Now;
            _tissueImageDal.Update(tissueImage);
            return new SuccessResult<TissueImage>(Messages.success);
        }

        private IResult<TissueImage> ImageCountChecker(int tissueid)
        {
            var result = _tissueImageDal.GetAll(p => p.TissueId == tissueid).Count;

            if (result == 5)
            {
                return new FailResult<TissueImage>(Messages.imageCountExceed);
            }
            return new SuccessResult<TissueImage>(Messages.success);
        }

        private IResult<TissueImage> IfImageExistCheck(int tissueId)
        {
            var result = _tissueImageDal.GetAll(p => p.TissueId == tissueId).Count;

            if (result == 0)
            {
                return new FailResult<TissueImage>(Messages.imageNotExists);
            }
            return new SuccessResult<TissueImage>(Messages.success);
        }
    }
}

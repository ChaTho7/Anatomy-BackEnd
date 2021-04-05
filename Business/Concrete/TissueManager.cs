using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Consts;
using Business.Validations.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Result;
using DataAccess.Abstract;
using Entities;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Internal;

namespace Business.Concrete
{
    public class TissueManager : ITissueService
    {
        ITissueDal _tissueDal;
        private ISortService _sortService;

        public TissueManager(ITissueDal tissueDal, ISortService sortService)
        {
            _tissueDal = tissueDal;
            _sortService = sortService;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(TissueValidator))]
        public IResult<Tissue> Add(Tissue tissue)
        {
            var result = BusinessRules<Tissue>.Checker(DuplicateNameChecker(tissue.Name));

            if (result != null)
            {
                foreach (var error in result)
                {
                    return new FailResult<Tissue>(error.Message);
                }
            }

            _tissueDal.Add(tissue);
            return new SuccessResult<Tissue>(Messages.success, tissue);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITissueService.GetById")]
        public IResult<Tissue> Delete(Tissue tissue)
        {
            return new SuccessResult<Tissue>(Messages.success, tissue);
        }

        [SecuredOperation("admin")]
        [CacheRemoveAspect("ITissueService.GetById")]
        public IResult<Tissue> Update(Tissue tissue)
        {
            _tissueDal.Update(tissue);
            return new SuccessResult<Tissue>(Messages.success, tissue);
        }

        [SecuredOperation("admin")]
        [CacheAspect]
        public IResult<Tissue> GetById(int id)
        {
            return new SuccessResult<Tissue>(Messages.success, _tissueDal.Get(p => p.Id == id));
        }

        [SecuredOperation("admin")]
        public IResult<List<TissueDetailDto>> GetByFilter(int? id, int? sortId, int? regionId)
        {
            return new SuccessResult<List<TissueDetailDto>>(Messages.success, _tissueDal.GetDetailByFilter(id, sortId, regionId));
        }

        [SecuredOperation("admin")]
        public IResult<List<Tissue>> GetAll()
        {
            return new SuccessResult<List<Tissue>>(Messages.success, _tissueDal.GetAll());
        }

        private IResult<Tissue> DuplicateNameChecker(string productName)
        {
            var result = (_tissueDal.GetAll(p => p.Name == productName).Any());

            if (result)
            {
                return new FailResult<Tissue>(Messages.duplicateName);
            }
            return new SuccessResult<Tissue>(Messages.success);
        }
    }
}

using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

//using Z.EntityFramework.Extensions;

namespace Core.Utilities.Interceptors
{
    public class StringTrimmerInterceptor : DbCommandInterceptor
    {
        /*public void StringTrimmer(DbCommand command, CommandEventData eventData)
        {
            var entities = eventData.Context.Model.GetEntityTypes();
            List<string> updatedStringNames = new List<string>();

            foreach (IEntityType entity in entities)
            {
                if (command.CommandText.Contains(entity.Name))
                {
                    var properties = entity.ClrType.GetProperties();
                    foreach (var property in properties)
                    {
                        if (command.CommandText.Contains(property.Name) && property.PropertyType.Name == "string")
                        {
                            updatedStringNames.Add(property.Name);
                        }
                    }
                }
            }

            foreach (var updatedStringName in updatedStringNames)
            {
                string trimName = $"TRIM([t].[{updatedStringName}])";
                command.CommandText = command.CommandText.Replace($"[t].[{updatedStringNames}]", trimName);
            }

            //string trimName = "RTRIM([t].[Name])";
            //if (command.CommandText.Contains("[t].[Name]"))
            //{
            //    string komut = command.CommandText.Replace("[t].[Name]", trimName);
            //    command.CommandText = komut;
            //}
        }

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData,
            InterceptionResult<DbDataReader> result)
        {
            //var entities = eventData.Context.Model.GetEntityTypes();
            //foreach (IEntityType entity in entities)
            //{
            //    var properties = entity.ClrType.GetProperties();
            //    foreach (System.Reflection.PropertyInfo property in properties)
            //    {
            //        var propertyName = property.Name;
            //        var propertyTypeName = property.PropertyType.Name;
            //    }
            //}

            //StringTrimmer(command, eventData);
            return result;
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            var asd1 = result.FieldCount;//5
            var asd3 = result.GetFieldType(2);//.name="string"
            var asd4 = result.GetName(2);//Name
            var asd5 = result.GetSchemaTable();

            var asd6 = asd5.Columns.GetType();
            var asd7 = asd5.Columns;
            var asd8 = asd5.Rows;

            base.ReaderExecuted(command, eventData, result);
            return result;
        }

        public override InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData,
            InterceptionResult<DbCommand> result)
        {
            base.CommandCreating(eventData, result);
            return result;
        }

        public override InterceptionResult DataReaderDisposing(DbCommand command,
            DataReaderDisposingEventData eventData, InterceptionResult result)
        {
            base.DataReaderDisposing(command, eventData, result);
            return result;
        }

        public override InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData,
            InterceptionResult<object> result)
        {
            base.ScalarExecuting(command, eventData, result);
            return result;
        }

        //public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        //{
        //    StringTrimmer(command);
        //    base.ReaderExecuting(command, interceptionContext);
        //}*/
    }
}

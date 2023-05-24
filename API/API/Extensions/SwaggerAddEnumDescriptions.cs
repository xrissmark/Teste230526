using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System;
using System.Linq;

namespace API.Extensions
{
    public class SwaggerAddEnumDescriptions : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // add enum descriptions to result models
            foreach (var property in swaggerDoc.Components.Schemas.Where(x => x.Value?.Enum?.Count > 0))
            {
                IList<IOpenApiAny> propertyEnums = property.Value.Enum;
                if (propertyEnums != null && propertyEnums.Count > 0)
                {
                    property.Value.Description += DescribeEnum(propertyEnums, property.Key);
                }
            }

            // add enum descriptions to input parameters
            foreach (var pathItem in swaggerDoc.Paths.Values)
            {
                DescribeEnumParameters(pathItem.Operations, swaggerDoc);
            }
        }

        private void DescribeEnumParameters(IDictionary<OperationType, OpenApiOperation> operations, OpenApiDocument swaggerDoc)
        {
            if (operations != null)
            {
                foreach (var oper in operations)
                {
                    foreach (var param in oper.Value.Parameters)
                    {
                        var paramEnum = swaggerDoc.Components.Schemas.FirstOrDefault(x => x.Key == param.Name);
                        if (paramEnum.Value != null)
                        {
                            param.Description += DescribeEnum(paramEnum.Value.Enum, paramEnum.Key);
                        }
                    }
                }
            }
        }

        private Type GetEnumTypeByName(string enumTypeName)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .FirstOrDefault(x => x.Name == enumTypeName);
        }

        private string DescribeEnum(IList<IOpenApiAny> enums, string proprtyTypeName)
        {
            List<string> enumDescriptions = new List<string>();
            var enumType = GetEnumTypeByName(proprtyTypeName);
            if (enumType == null)
                return null;

            foreach (IOpenApiAny enumOption in enums)
            {
                if (enumOption is OpenApiString @string)
                {
                    string enumString = @string.Value;

                    enumDescriptions.Add(string.Format("{0} = {1}", (int)Enum.Parse(enumType, enumString), enumString));
                }
                else if (enumOption is OpenApiInteger integer)
                {
                    int enumInt = integer.Value;

                    enumDescriptions.Add(string.Format("{0} = {1}", enumInt, Enum.GetName(enumType, enumInt)));
                }
            }

            return string.Join(", ", enumDescriptions.ToArray());
        }
    }
}

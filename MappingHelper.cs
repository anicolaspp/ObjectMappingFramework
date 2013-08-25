﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MappingObjectsFramework
{
    public static class MappingHelper
    {
        public static T MapTo<T>(this object obj, IEnumerable<Converter> converters) where T : new()
        {
            ConverterResolver converterResolver = new StandartConverterResolver(true);

            foreach (var converter in converters)
            {
                converterResolver.Add(converter);
            }

            return MapTo<T>(obj, converterResolver);
        }

        public static T MapTo<T>(this object obj, ConverterResolver converterResolver) where T : new()
        {
            if (converterResolver == null)
            {
                throw new ArgumentNullException("converterResolver");
            }

            T result = new T();

            foreach (var propertyInfo in result.GetType().GetProperties())
            {
                MapPropertyNameAttribute t = (MapPropertyNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(MapPropertyNameAttribute));

                if (t != null)
                {
                    PropertyInfo sourceProperty = t.Type == null ? obj.GetType().GetProperty(t.Name) : obj.GetType().GetProperty(t.Name, t.Type);

                    if (sourceProperty == null)
                    {
                        throw new InvalidMapException(string.Format("There is not a property on {0} object which matches with {1} from the target object", obj, t.Name));
                    }

                    try
                    {
                        Type outType = propertyInfo.PropertyType;
                        Type inType = sourceProperty.PropertyType;

                        object valueToConvert = sourceProperty.GetValue(obj, null);

                        if (valueToConvert != null)
                        {
                            MethodInfo genericMethod = converterResolver.GetType().GetMethod("Convert").MakeGenericMethod(inType, outType);
                            object valueToSet = genericMethod.Invoke(converterResolver, new[] {valueToConvert});

                            propertyInfo.SetValue(result, valueToSet, null);
                        }
                        else
                        {
                            Expression<Func<object>> e = Expression.Lambda<Func<object>>(Expression.Convert(Expression.Default(outType), outType));

                            object defaultValue = e.Compile()(); //outType.IsValueType ? Activator.CreateInstance(outType) : null;
                            propertyInfo.SetValue(result, defaultValue, null);
                        }

                    }
                    catch (Exception e)
                    {
                        throw new InvalidMapException(string.Format("The types of the properties {0} on {1} and {2} on {3} do not match. " +
                                                                    "See Inner Exception for more details.", propertyInfo.Name,
                            result.GetType().Name, sourceProperty.Name, obj.GetType().Name), e);
                    }
                }
            }

            return result;
        }

        public static T MapTo<T>(this object obj, params Converter[] converters) where T : new()
        {
            if (converters.Length == 0)
                return MapTo<T>(obj, new DefaultConverterResolver());
            
            ConverterResolver converterResolver = new StandartConverterResolver(true);

            foreach (var converter in converters)
            {
                converterResolver.Add(converter);
            }

            return MapTo<T>(obj, converterResolver);

            #region old

            //T result = new T();

            //foreach (var propertyInfo in result.GetType().GetProperties())
            //{
            //    PropertyNameAttribute t = (PropertyNameAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(PropertyNameAttribute));

            //    if (t != null)
            //    {
            //        PropertyInfo sourceProperty;

            //        if (t.Type == null)
            //        {
            //            sourceProperty = obj.GetType().GetProperty(t.Name);
            //        }
            //        else
            //        {
            //            sourceProperty = obj.GetType().GetProperty(t.Name, t.Type);
            //        }

            //        if (sourceProperty == null)
            //        {
            //            throw new InvalidMapException("There is not a property on the DB object which matches with " + t.Name + " from the domain object");
            //        }

            //        try
            //        {
            //            propertyInfo.SetValue(result, sourceProperty.GetValue(obj, null), null);
            //        }
            //        catch (Exception e)
            //        {
            //            throw new InvalidMapException(string.Format("The types of the properties {0} on {1} and {2} on {3} do not match. " +
            //                                                        "See Inner Exception for more details.", propertyInfo.Name,
            //                result.GetType().Name, sourceProperty.Name, obj.GetType().Name), e);
            //        }
            //    }
            //}

            //return result;

            #endregion
        }
    }
}
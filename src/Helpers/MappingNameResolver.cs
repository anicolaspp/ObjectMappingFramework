using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq;

namespace MappingObjectsFramework
{
    public class MappingNameResolver : List<NameMap>
    {
        private string this [string propertyName, bool fromSource]
        {
            get
            {
                try
                {
                    var targetName = fromSource == true ? this.FirstOrDefault(x => x.SourceProperty == propertyName).TargetProperty :
                        this.FirstOrDefault(x => x.TargetProperty == propertyName).SourceProperty;

                    return targetName;
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public string this [string sourcePropertyName]
        {
            get
            {
                return this[sourcePropertyName, true];
            }
        }

        public string GetSource(string targetPropertyName)
        {
            return this[targetPropertyName, false];
        }
    }
}
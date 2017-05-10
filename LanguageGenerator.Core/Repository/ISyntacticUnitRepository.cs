using System.Collections.Generic;
using LanguageGenerator.Core.SyntacticProperty;
using LanguageGenerator.Core.SyntacticProperty.ParentProperty;
using LanguageGenerator.Core.SyntacticUnit;


namespace LanguageGenerator.Core.Repository
{
    public interface ISyntacticUnitRepository
    {
        ISet<IProperty> Properties { get;  }
        ISet<ISyntacticUnit> SyntacticUnits { get;  }

        IProperty GetPropertyWithName(string propertyName);
        IParentProperty GetParentPropertyWithName(string propertyName);
        IRootProperty GetRootPropertyWithName(string propertyName);
    }
}
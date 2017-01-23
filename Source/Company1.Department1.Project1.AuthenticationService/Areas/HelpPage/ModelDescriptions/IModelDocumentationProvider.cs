using System;
using System.Reflection;

namespace Company1.Department1.Project1.AuthenticationService.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}
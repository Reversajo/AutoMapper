using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Configuration.Conventions;

namespace AutoMapper
{
    /// <summary>
    /// Configuration for profile-specific maps
    /// </summary>
    public interface IProfileExpression
    {
        /// <summary>
        /// Disable constructor mapping. Use this if you don't intend to have AutoMapper try to map to constructors
        /// </summary>
        void DisableConstructorMapping();

        /// <summary>
        /// Creates a mapping configuration from the <typeparamref name="TSource"/> type to the <typeparamref name="TDestination"/> type
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <returns>Mapping expression for more configuration options</returns>
        IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>();

        /// <summary>
        /// Creates a mapping configuration from the <typeparamref name="TSource"/> type to the <typeparamref name="TDestination"/> type.
        /// Specify the member list to validate against during configuration validation.
        /// </summary>
        /// <typeparam name="TSource">Source type</typeparam>
        /// <typeparam name="TDestination">Destination type</typeparam>
        /// <param name="memberList">Member list to validate</param>
        /// <returns>Mapping expression for more configuration options</returns>
        IMappingExpression<TSource, TDestination> CreateMap<TSource, TDestination>(MemberList memberList);

        /// <summary>
        /// Create a mapping configuration from the source type to the destination type.
        /// Use this method when the source and destination type are known at runtime and not compile time.
        /// </summary>
        /// <param name="sourceType">Source type</param>
        /// <param name="destinationType">Destination type</param>
        /// <returns>Mapping expression for more configuration options</returns>
        IMappingExpression CreateMap(Type sourceType, Type destinationType);

        /// <summary>
        /// Creates a mapping configuration from the source type to the destination type.
        /// Specify the member list to validate against during configuration validation.
        /// </summary>
        /// <param name="sourceType">Source type</param>
        /// <param name="destinationType">Destination type</param>
        /// <param name="memberList">Member list to validate</param>
        /// <returns>Mapping expression for more configuration options</returns>
        IMappingExpression CreateMap(Type sourceType, Type destinationType, MemberList memberList);

        /// <summary>
        /// Clear the list of recognized prefixes.
        /// </summary>
        void ClearPrefixes();

        /// <summary>
        /// Recognize a list of prefixes to be removed from source member names when matching
        /// </summary>
        /// <param name="prefixes">List of prefixes</param>
        void RecognizePrefixes(params string[] prefixes);

        /// <summary>
        /// Recognize a list of postfixes to be removed from source member names when matching
        /// </summary>
        /// <param name="postfixes">List of postfixes</param>
        void RecognizePostfixes(params string[] postfixes);

        /// <summary>
        /// Provide an alias for a member name when matching source member names
        /// </summary>
        /// <param name="original">Original member name</param>
        /// <param name="alias">Alias to match against</param>
        void RecognizeAlias(string original, string alias);


        /// <summary>
        /// Provide a new value for a part of a members name
        /// </summary>
        /// <param name="original">Original member value</param>
        /// <param name="newValue">New member value</param>
        void ReplaceMemberName(string original, string newValue);

        /// <summary>
        /// Recognize a list of prefixes to be removed from destination member names when matching
        /// </summary>
        /// <param name="prefixes">List of prefixes</param>
        void RecognizeDestinationPrefixes(params string[] prefixes);

        /// <summary>
        /// Recognize a list of postfixes to be removed from destination member names when matching
        /// </summary>
        /// <param name="postfixes">List of postfixes</param>
        void RecognizeDestinationPostfixes(params string[] postfixes);

        /// <summary>
        /// Add a property name to globally ignore. Matches against the beginning of the property names.
        /// </summary>
        /// <param name="propertyNameStartingWith">Property name to match against</param>
        void AddGlobalIgnore(string propertyNameStartingWith);

        /// <summary>
        /// Allow null destination values. If false, destination objects will be created for deep object graphs. Default true.
        /// </summary>
        bool? AllowNullDestinationValues { get; set; }

        /// <summary>
        /// Allow null destination collections. If true, null source collections result in null destination collections. Default false.
        /// </summary>
        bool? AllowNullCollections { get; set; }

        /// <summary>
        /// Allows to enable null-value propagation for query mapping. 
        /// <remarks>Some providers (such as EntityFrameworkQueryVisitor) do not work with this feature enabled!</remarks>
        /// </summary>
        bool? EnableNullPropagationForQueryMapping { get; set; }

        /// <summary>
        /// Naming convention for source members
        /// </summary>
        INamingConvention SourceMemberNamingConvention { get; set; }

        /// <summary>
        /// Naming convention for destination members
        /// </summary>
        INamingConvention DestinationMemberNamingConvention { get; set; }

        /// <summary>
        /// Specify common configuration for all type maps.
        /// </summary>
        /// <param name="configuration">configuration callback</param>
        void ForAllMaps(Action<TypeMap, IMappingExpression> configuration);

        /// <summary>
        /// Customize configuration for all members across all maps
        /// </summary>
        /// <param name="condition">Condition</param>
        /// <param name="memberOptions">Callback for member options. Use the property map for conditional maps.</param>
        void ForAllPropertyMaps(Func<PropertyMap, bool> condition, Action<PropertyMap, IMemberConfigurationExpression> memberOptions);

        Func<PropertyInfo, bool> ShouldMapProperty { get; set; }
        Func<FieldInfo, bool> ShouldMapField { get; set; }
        Func<MethodInfo, bool> ShouldMapMethod { get; set; }
        Func<ConstructorInfo, bool> ShouldUseConstructor { get; set; }
        
        string ProfileName { get; }
        IMemberConfiguration AddMemberConfiguration();

        /// <summary>
        /// Include extension methods against source members for matching destination members to. Default source extension methods from <see cref="System.Linq.Enumerable"/>
        /// </summary>
        /// <param name="type">Static type that contains extension methods</param>
        void IncludeSourceExtensionMethods(Type type);

        /// <summary>
        /// Value transformers. Modify the list directly or use <see cref="ValueTransformerConfigurationExtensions.Add{TValue}"/>
        /// </summary>
        IList<ValueTransformerConfiguration> ValueTransformers { get; }
    }
}
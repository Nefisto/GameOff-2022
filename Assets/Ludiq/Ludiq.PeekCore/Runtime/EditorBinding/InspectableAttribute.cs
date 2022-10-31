using System;

namespace Ludiq.PeekCore
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
	public sealed class InspectableAttribute : Attribute, IInspectableAttribute
	{
		public InspectableAttribute() { }

		public int order { get; set; }
	}
}
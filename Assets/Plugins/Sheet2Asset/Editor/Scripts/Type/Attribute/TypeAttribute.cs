namespace Sheet2Asset.Type {
	public class TypeAttribute : System.Attribute {
		public System.Type type;

		public TypeAttribute(System.Type Type) {
			type = Type;
		}
	}
}

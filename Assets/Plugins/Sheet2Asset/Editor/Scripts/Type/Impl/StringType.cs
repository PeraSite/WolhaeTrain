namespace Sheet2Asset.Type {
	[Type(typeof(string))]
	public class StringType : IType {
		public object DefaultValue => string.Empty;

		public object Read(string value) {
			return value;
		}


		public string Write(object value) {
			return value.ToString();
		}
	}
}

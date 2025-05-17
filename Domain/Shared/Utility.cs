namespace Domain.Shared;

public static class Utility
{
	#region Static Constructor
	static Utility()
	{
	}
	#endregion /Static Constructor

	#region Consts
	public abstract class Const
	{
		private Const()
		{
		}

		public const byte UsernameMinLength = 3;

		public const byte UsernameMaxLength = 30;

		public const byte RoleNameMinLength = 3;

		public const byte RoleNameMaxLength = 50;

		public const byte PasswordMinLength = 8;

		public const byte PasswordMaxLength = 20;

		public const byte FullNameMinLength = 100;

		public const byte FullNameMaxLength = 100;

		public const byte AgeMaxLength = 120;

		public const byte AgeMinLength = 0;

		public const int DescriptionMaxLength = 500;

		public const int EmailMaxLength = 100;

		public const int ReasonMinLength = 10;

		public const int ReasonMaxLength = 3000;
	}
	#endregion /Consts

	#region Regex
	public abstract class Regex
	{
		private Regex()
		{
		}

		public const string Email = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
	}
	#endregion /Regex
}
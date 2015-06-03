namespace Gamelogic
{
	/**
		Classes that implement this interface can produce a new element of the 
		given type. Generators differ from enumerables in that they generally
		are infinite, and don't have a "start" position.
	
		@version1_2
	*/
	public interface IGenerator<out T>
	{
		/**
			Generates the next element.
		*/
		T Next();
	}
}
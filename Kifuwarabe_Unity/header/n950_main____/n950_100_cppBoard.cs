/*
class CppBoard {
public:
	// 枠も含めて碁盤を全走査
	template<typename Func> static void ForeachAllNodesWithWaku(int boardSize, Func func)
	{
		for (int x = 0; x < boardSize + 2; x++)
		{
			for (int y = 0; y < boardSize + 2; y++)
			{
				func(x, y);
			}
		}
	}

	// 枠を含めない碁盤を全走査
	template<typename Func> static void ForeachAllNodesWithoutWaku(int boardSize, Func func)
	{
		for (int x = 1; x < boardSize + 1; x++)
		{
			for (int y = 1; y < boardSize + 1; y++)
			{
				func(x, y);
			}
		}
	}
};
*/

//# include <windows.h> // コンソールへの出力等
using n190_board___;///n190_100_board.h"
using n190_board___;///n190_150_liberty.h"
using n750_explain_;///n750_100_figureType.h"
using n800_scene___;///n800_500_endgame.h"


namespace n800_scene___
{

    int Endgame::EndgameStatus(int arr_endgameBoard[], Board* pBoard)
    {
        pBoard->ForeachAllNodesWithoutWaku([&arr_endgameBoard, &pBoard](int node, bool & isBreak) {

            // ポイント☆？
            int* ptr = arr_endgameBoard + node;

            int color = pBoard->ValueOf(node);
            if (color == EMPTY) {
                *ptr = GTP_DAME;
                int sum = 0;
                pBoard->ForeachArroundNodes(node, [&pBoard, &sum](int adjNode, bool & isBreak) {
                    int adjColor;   // 隣接(adjacent)する石の色
                    adjColor = pBoard->ValueOf(adjNode);
                    if (adjColor == WAKU) {
                        goto gt_Next;
                    }
                    sum |= adjColor;

                    gt_Next:
                    ;
                });

                if (sum == BLACK) {
                    // 黒字☆
                    *ptr = GtpStatusType::GTP_BLACK_TERRITORY;
                }
                if (sum == WHITE) {
                    // 白地☆
                    *ptr = GtpStatusType::GTP_WHITE_TERRITORY;
                }
            }
            else {
                *ptr = GTP_ALIVE;

                Liberty liberty;

                liberty.Count(node, color, pBoard);
                //	core.PRT( "(%2d,%2d),ishi=%2d,dame=%2d\n",z&0xff,z>>8,ishi,dame);

                if (liberty.liberty <= 1) {
                    // 石は死石☆
                    *ptr = GtpStatusType::GTP_DEAD;
                }
            }
        });

        return 0;
    }

    int Endgame::EndgameDrawFigure(int arr_endgameBoard[], Board* pBoard)
    {
        int x;
        int y;
        int node;
        int* ptr;

        for (y = 1; y < pBoard->GetSize() + 1; y++) {
            for (x = 1; x < pBoard->GetSize() + 1; x++) {
                node = Board::ConvertToNode(x, y);
                ptr = arr_endgameBoard + node;
                if ((rand() % 2) == 0) {
                    *ptr = FigureType::FIGURE_NONE;
                }
                else {
                    if (rand() % 2) {
                        *ptr = FigureType::FIGURE_BLACK;
                    }
                    else {
                        *ptr = FigureType::FIGURE_WHITE;
                    }
                    *ptr |= (rand() % 9) + 1;
                }
            }
        }
        return 0;
    }

    int Endgame::EndgameDrawNumber(int arr_endgameBoard[], Board* pBoard)
    {
        int x;
        int y;
        int node;
        int* ptr;

        for (y = 1; y < pBoard->GetSize() + 1; y++) {
            for (x = 1; x < pBoard->GetSize() + 1; x++) {
                node = Board::ConvertToNode(x, y);
                ptr = arr_endgameBoard + node;
                *ptr = (rand() % 110) - 55;
            }
        }
        return 0;
    }

}
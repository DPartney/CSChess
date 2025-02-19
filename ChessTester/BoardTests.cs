using ChessLibrary;
using System.Collections;

namespace ChessTester
{
    [TestClass]
    public sealed class BoardTests
    {
        [TestMethod]
        public void ChessBoardIsNotNull()
        {
            Board board = new Board();
            
            board.Init(false);

            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void ChessBoardHasCorrectPiece()
        {
            Board board = new Board();
            
            board.Init(false);

            Assert.IsTrue(board["a1"].piece.IsRook());
        }

        [TestMethod]
        public void Chess960BoardIsNotNull()
        {
            Board board960 = new Board();
            
            board960.Init(true);

            Assert.IsNotNull(board960);
        }

        [TestMethod]
        public void Chess960BoardHasCorrectPieceCount()
        {
            Board board960 = new Board();
            int foundPieceCount = 0;
            
            board960.Init(true);
            for (int col = 1; col <= 8; col++)
            {
                if (!board960[1, col].piece.IsEmpty())
                {
                    foundPieceCount++;
                }
            }

            Assert.AreEqual(8, foundPieceCount);
        }

        [TestMethod]
        public void GameModesProduceDifferentBoards()
        {
            Board board = new Board();
            Board board960 = new Board();

            board.Init(false);
            board960.Init(true);

            Assert.AreNotEqual<Board>(board, board960);
        }
    }
}

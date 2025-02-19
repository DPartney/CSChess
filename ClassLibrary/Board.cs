/***************************************************************
 * File: Board.cs
 * Created By: Justin Grindal		Date: 27 June, 2013
 * Description: The main chess board. Board contain the chess cell
 * which will contain the chess pieces. Board also contains the methods
 * to get and set the user moves.
 ***************************************************************/

using System;
using System.Collections;
using System.Xml;

namespace ChessLibrary
{
	/// <summary>
	/// he main chess board. Board contain the chess cell
	/// which will contain the chess pieces. Board also contains the methods
	/// to get and set the user moves.
	/// </summary>
    [Serializable]
	public class Board
	{
		private Side m_WhiteSide, m_BlackSide;	// Chess board site object 
		private Cells m_cells;	// collection of cells in the board

		public Board()
		{
            m_WhiteSide = new Side(Side.SideType.White);	// Makde white side
            m_BlackSide = new Side(Side.SideType.Black);	// Makde white side

			m_cells = new Cells();					// Initialize the chess cells collection
		}

		// Initialize the chess board and place piece on thier initial positions
		public void Init(bool Enable960)
		{
			m_cells.Clear();		// Remove any existing chess cells

			// Build the 64 chess board cells
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					m_cells.Add(new Cell(row,col));	// Initialize and add the new chess cell
				}
			
			if (Enable960)
			{
				Random rand = new Random();
				Piece.PieceType[] generatedPositions = new Piece.PieceType[8];
				int index1 = 0;
				int index2 = 0;
				bool foundRoyalIndex = false;
				
				while (true)
				{
					index1 = rand.Next(8);
					index2 = rand.Next(8);
					if (!(index1 % 2 == 0) && (index2 % 2 == 0)) break; // Checks if bishop1 is odd then checks if bishop2 is even, if both are true break
                }

				generatedPositions[index1] = Piece.PieceType.Bishop;
				generatedPositions[index2] = Piece.PieceType.Bishop;

				for (int i = 0; i < 3; i++)
				{
					do
					{
						index1 = rand.Next(8);
					}
					while (generatedPositions[index1] != Piece.PieceType.Empty); // Checks for a valid position
					{
						if (!foundRoyalIndex)
						{
							generatedPositions[index1] = Piece.PieceType.Queen; // Places queen if one hasn't been placed
                            foundRoyalIndex = true;	// Prevents multiple queens
						}
						else generatedPositions[index1] = Piece.PieceType.Knight; // Places knight after queen has been placed
					}
				}

				foundRoyalIndex = true;
				for (int i = 0; i < 3; i++)
				{
					index1 = 0;
					for (;  index1 < 8; index1++)
					{
						if (generatedPositions[index1] == Piece.PieceType.Empty) break;
					}

					if (!foundRoyalIndex)
					{
						generatedPositions[index1] = Piece.PieceType.King;
						foundRoyalIndex = true;
					}
					else
					{
                        generatedPositions[index1] = Piece.PieceType.Rook;
                        foundRoyalIndex = false;
                    }
				}

				// Now setup the board for black side
				m_cells["a1"].piece = new Piece(generatedPositions[0], m_BlackSide);
                m_cells["b1"].piece = new Piece(generatedPositions[1], m_BlackSide);
                m_cells["c1"].piece = new Piece(generatedPositions[2], m_BlackSide);
                m_cells["d1"].piece = new Piece(generatedPositions[3], m_BlackSide);
                m_cells["e1"].piece = new Piece(generatedPositions[4], m_BlackSide);
                m_cells["f1"].piece = new Piece(generatedPositions[5], m_BlackSide);
                m_cells["g1"].piece = new Piece(generatedPositions[6], m_BlackSide);
                m_cells["h1"].piece = new Piece(generatedPositions[7], m_BlackSide);
                for (int col = 1; col <= 8; col++) m_cells[2, col].piece = new Piece(Piece.PieceType.Pawn, m_BlackSide);

                // Now setup the board for white side
                m_cells["a8"].piece = new Piece(generatedPositions[0], m_WhiteSide);
                m_cells["b8"].piece = new Piece(generatedPositions[1], m_WhiteSide);
                m_cells["c8"].piece = new Piece(generatedPositions[2], m_WhiteSide);
                m_cells["d8"].piece = new Piece(generatedPositions[3], m_WhiteSide);
                m_cells["e8"].piece = new Piece(generatedPositions[4], m_WhiteSide);
                m_cells["f8"].piece = new Piece(generatedPositions[5], m_WhiteSide);
                m_cells["g8"].piece = new Piece(generatedPositions[6], m_WhiteSide);
                m_cells["h8"].piece = new Piece(generatedPositions[7], m_WhiteSide);
                for (int col = 1; col <= 8; col++) m_cells[7, col].piece = new Piece(Piece.PieceType.Pawn, m_WhiteSide);
            }
			else
			{
				// Now setup the board for black side
				m_cells["a1"].piece = new Piece(Piece.PieceType.Rook, m_BlackSide);
				m_cells["h1"].piece = new Piece(Piece.PieceType.Rook,m_BlackSide);
				m_cells["b1"].piece = new Piece(Piece.PieceType.Knight,m_BlackSide);
				m_cells["g1"].piece = new Piece(Piece.PieceType.Knight,m_BlackSide);
				m_cells["c1"].piece = new Piece(Piece.PieceType.Bishop,m_BlackSide);
				m_cells["f1"].piece = new Piece(Piece.PieceType.Bishop,m_BlackSide);
				m_cells["e1"].piece = new Piece(Piece.PieceType.King,m_BlackSide);
				m_cells["d1"].piece = new Piece(Piece.PieceType.Queen,m_BlackSide);
				for (int col=1; col<=8; col++) m_cells[2, col].piece = new Piece(Piece.PieceType.Pawn,m_BlackSide);

				// Now setup the board for white side
				m_cells["a8"].piece = new Piece(Piece.PieceType.Rook,m_WhiteSide);
				m_cells["h8"].piece = new Piece(Piece.PieceType.Rook,m_WhiteSide);
				m_cells["b8"].piece = new Piece(Piece.PieceType.Knight,m_WhiteSide);
				m_cells["g8"].piece = new Piece(Piece.PieceType.Knight,m_WhiteSide);
				m_cells["c8"].piece = new Piece(Piece.PieceType.Bishop,m_WhiteSide);
				m_cells["f8"].piece = new Piece(Piece.PieceType.Bishop,m_WhiteSide);
				m_cells["e8"].piece = new Piece(Piece.PieceType.King,m_WhiteSide);
				m_cells["d8"].piece = new Piece(Piece.PieceType.Queen,m_WhiteSide);
				for (int col=1; col<=8; col++) m_cells[7, col].piece = new Piece(Piece.PieceType.Pawn,m_WhiteSide);
			}
		}

		// get the new item by rew and column
		public Cell this[int row, int col]
		{
			get
			{
				return m_cells[row, col];
			}
		}

		// get the new item by string location
		public Cell this[string strloc]
		{
			get
			{
				return m_cells[strloc];	
			}
		}

		// get the chess cell by given cell
		public Cell this[Cell cellobj]
		{
			get
			{
				return m_cells[cellobj.ToString()];	
			}
		}

        /// <summary>
        /// Serialize the Game object as XML String
        /// </summary>
        /// <returns>XML containing the Game object state XML</returns>
        public XmlNode XmlSerialize(XmlDocument xmlDoc)
        {
            XmlElement xmlBoard = xmlDoc.CreateElement("Board");

            // Append game state attributes
            xmlBoard.AppendChild(m_WhiteSide.XmlSerialize(xmlDoc));
            xmlBoard.AppendChild(m_BlackSide.XmlSerialize(xmlDoc));

            xmlBoard.AppendChild(m_cells.XmlSerialize(xmlDoc));

            // Return this as String
            return xmlBoard;
        }

        /// <summary>
        /// DeSerialize the Board object from XML String
        /// </summary>
        /// <returns>XML containing the Board object state XML</returns>
        public void XmlDeserialize(XmlNode xmlBoard)
        {
            // Deserialize the Sides XML
            XmlNode side = XMLHelper.GetFirstNodeByName(xmlBoard, "Side");

            // Deserialize the XML nodes
            m_WhiteSide.XmlDeserialize(side);
            m_BlackSide.XmlDeserialize(side.NextSibling);

            // Deserialize the Cells
            XmlNode xmlCells = XMLHelper.GetFirstNodeByName(xmlBoard, "Cells");
            m_cells.XmlDeserialize(xmlCells);
        }

		// get all the cell locations on the chess board
		public ArrayList GetAllCells()
		{
			ArrayList CellNames = new ArrayList();

			// Loop all the squars and store them in Array List
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					CellNames.Add(this[row,col].ToString()); // append the cell name to list
				}

			return CellNames;
		}

		// get all the cell containg pieces of given side
        public ArrayList GetSideCell(Side.SideType PlayerSide)
		{
			ArrayList CellNames = new ArrayList();

			// Loop all the squars and store them in Array List
			for (int row=1; row<=8; row++)
				for (int col=1; col<=8; col++)
				{
					// check and add the current type cell
					if (this[row,col].piece!=null && !this[row,col].IsEmpty() && this[row,col].piece.Side.type == PlayerSide)
						CellNames.Add(this[row,col].ToString()); // append the cell name to list
				}

			return CellNames;
		}

		// Returns the cell on the top of the given cell
		public Cell TopCell(Cell cell)
		{
			return this[cell.row-1, cell.col];
		}

		// Returns the cell on the left of the given cell
		public Cell LeftCell(Cell cell)
		{
			return this[cell.row, cell.col-1];
		}

		// Returns the cell on the right of the given cell
		public Cell RightCell(Cell cell)
		{
			return this[cell.row, cell.col+1];
		}

		// Returns the cell on the bottom of the given cell
		public Cell BottomCell(Cell cell)
		{
			return this[cell.row+1, cell.col];
		}

		// Returns the cell on the top-left of the current cell
		public Cell TopLeftCell(Cell cell)
		{
			return this[cell.row-1, cell.col-1];
		}

		// Returns the cell on the top-right of the current cell
		public Cell TopRightCell(Cell cell)
		{
			return this[cell.row-1, cell.col+1];
		}

		// Returns the cell on the bottom-left of the current cell
		public Cell BottomLeftCell(Cell cell)
		{
			return this[cell.row+1, cell.col-1];
		}

		// Returns the cell on the bottom-right of the current cell
		public Cell BottomRightCell(Cell cell)
		{
			return this[cell.row+1, cell.col+1];
		}
	}
}

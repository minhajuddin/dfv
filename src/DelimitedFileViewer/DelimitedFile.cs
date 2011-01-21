using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace DelimitedFileViewer
{
    class DelimitedFile
    {
        //Variables
        private string _filename;
        private string _delimiter;
        private Encoding _fileEncoding;
        private bool _toggleHeader;

        //Constructor
        public DelimitedFile(string filename, string delimiter, Encoding fileEncoding, bool toggleHeader)
        {
            this._filename = filename;
            this._delimiter = delimiter;
            this._fileEncoding = fileEncoding;
            this._toggleHeader = toggleHeader;
        }

        //Functions
        public DataTable ReadFile()
        {
            //This function reads the delimited file using properties in this object and returns a data table
            DataTable dt = new DataTable();
            string currentLine;
            int numberOfColumns=0;
            char delimiter = char.Parse(_delimiter);

            using (StreamReader sr = new StreamReader(_filename, _fileEncoding))
            {
                while (!sr.EndOfStream)
                {
                    currentLine = sr.ReadLine();
                    //If the number of columns is less than the number of columns in the current line replace it.
                    numberOfColumns = (numberOfColumns < (currentLine.Split(delimiter).Length)) ? currentLine.Split(delimiter).Length : numberOfColumns;
                    string[] temp = currentLine.Split(delimiter);
                }
                sr.Close();
                sr.Dispose();
            }

            

            using (StreamReader sr = new StreamReader(_filename, _fileEncoding))
            {
                //Name the columns for the dataset
                string[] columnNames = new string[numberOfColumns];
                if (_toggleHeader)
                {
                    string[] tempCols = sr.ReadLine().Split(delimiter);
                    if (tempCols.Length == numberOfColumns)
                        columnNames = tempCols;
                    else
                        tempCols.CopyTo(columnNames, 0);

                    //Replace NULL column names with a name
                    for (int i = 1; i <= numberOfColumns; i++)
                    {
                        columnNames[i - 1] = (columnNames[i - 1] == null) ? "Column" + i.ToString() : columnNames[i - 1];
                    }

                }
                else
                {
                    for (int i = 1; i <= numberOfColumns; i++)
                    {
                        columnNames[i - 1] = "Column" + i.ToString();
                    }
                }

                //Create the metadata for the dataset.
                for (int i = 0; i < numberOfColumns; i++)
                {
                    dt.Columns.Add(columnNames[i], Type.GetType("System.String"));
                }

                //Load the dataset with the data.
                while (!sr.EndOfStream)
                {
                    dt.Rows.Add(sr.ReadLine().Split(delimiter));
                }

            }

            return dt;
        }

        public FileStatus Validate()
        {
            //This function validates the object
            if (string.IsNullOrEmpty(this._filename) || (!File.Exists(this._filename)))
            {
                return FileStatus.FileDoesNotExist;
            }

            if (string.IsNullOrEmpty(this._delimiter))
            {
                return FileStatus.IncorrectDelimiter;
            }

            //Check the encoding too

            return FileStatus.Valid;
        }
    }
}

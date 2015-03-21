using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMVP
{
    public class Track
    {
        private string _name;//Name of the track.
        private string _s;   //The string extracted from the file.
        private int _p = 0;  //A pointer to a character in the string.
        private int _size;   //The number of vertices in the matrix being read.
        private int[,] _m;   //The matrix containing the track.

        /// <summary>
        /// TrackImporter is used to import tracks stored as .txt files.
        /// </summary>
        /// <param name="file"> The full path of the file (ex. C:\Documents\track.txt). </param>
        public Track(string file)
        {
            //Read the file:
            System.IO.StreamReader sr = new System.IO.StreamReader(file);
            _s = sr.ReadToEnd();
            sr.Close();

            //Get name of the track:
            _name = getNextLine();

            //Get size of the matrix and create an array accordingly:
            _size = Convert.ToInt32(getNextWord());
            _m = new int[3, _size];

            //Extract data from matrix:
            for(int i = 0; i < _size; i++)
            {
                _m[0, i] = Convert.ToInt32(getNextWord());
                _m[1, i] = Convert.ToInt32(getNextWord());
                _m[2, i] = Convert.ToInt32(getNextWord());
                //Console.WriteLine(_m[0, i] + "\t" + _m[1, i] + "\t" + _m[2, i]);
            }
            
        }
        
        /// <summary>
        /// Returns the next word following the character pointed to by 'p'. This will also advance 'p' to the beginning of the next word.
        /// </summary>
        /// <returns></returns>
        private string getNextWord()
        {
            StringBuilder sb = new StringBuilder();

            while (_s[_p] != ' ' && _s[_p] != '\n')
            {
                sb.Append(_s[_p]);
                _p++;
            }

            while ((_p < _s.Length) && (_s[_p] == ' ' || _s[_p] == '\n'))
            {
                _p++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns the remaining part of the entire line, folloing where 'p' is pointing, ei. it will return all characters until the next '\n'. 
        /// </summary>
        /// <returns></returns>
        private string getNextLine()
        {
            StringBuilder sb = new StringBuilder();

            while (_s[_p] != '\n')
            {
                sb.Append(_s[_p]);
                _p++;
            }

            while ((_p < _s.Length) && (_s[_p] == ' ' || _s[_p] == '\n'))
            {
                _p++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// The matrix representing the track as an 3 * X int array, where X is the number of vertices in the track.
        /// </summary>
        public int[,] m
        {
            get { return _m; }
        }

        /// <summary>
        /// Name of the track as specified in the file.
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}

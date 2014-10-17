using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System;
using System.Text;
using System.IO;

namespace SS.Ynote.Classic.Core.UI
{
//---------------------------------------------------------------------------------
//IPEWrapper.cs - version 2.7.6.0r
//BY DOWNLOADING AND USING, YOU AGREE TO THE FOLLOWING TERMS:
//Copyright (c) 2006-2008 by Joseph P. Socoloski III
//LICENSE
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.
//the MIT License, given here: <http://www.opensource.org/licenses/mit-license.php> 
//---------------------------------------------------------------------------------
    
    public delegate void Response(string text);

    public class IpeStreamWrapper : Stream
    {
        private MemoryStream _stream = new MemoryStream();
        private readonly Response _response;

        public IpeStreamWrapper(Response response)
        {
            _response = response;
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override long Length
        {
            get { return _stream.Length; }
        }

        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        public override void Flush()
        {
            _stream.Flush();

            _stream.Seek(0, SeekOrigin.Begin);
            var sr = new StreamReader(_stream, Encoding.ASCII);
            _response(sr.ReadToEnd());
            sr.Close();
            _stream = new MemoryStream();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }

        public static StringBuilder Output = new StringBuilder();

        /// <summary>
        /// Method that allows capture of IronPython output
        /// </summary>
        /// <param name="text"></param>
        public static void EngineResponse(string text)
        {

            if (!string.IsNullOrEmpty(text.Trim()))
            {
                //Output.Remove(0, Output.Length);        //Clear
                text = text.Replace("\\n", "\r\n"); //to support newline for textbox use
                Output.Append(text + Environment.NewLine);
            }
        }
    }
    internal class CommandHistory
    {
        private int currentPosn;
        private string lastCommand;
        private ArrayList commandHistory = new ArrayList();

        internal CommandHistory()
        {
        }

        internal void Add(string command)
        {
            if (command != lastCommand)
            {
                commandHistory.Add(command);
                lastCommand = command;
                currentPosn = commandHistory.Count;
            }
        }

        internal bool DoesPreviousCommandExist()
        {
            return currentPosn > 0;
        }

        internal bool DoesNextCommandExist()
        {
            return currentPosn < commandHistory.Count - 1;
        }

        internal string GetPreviousCommand()
        {
            lastCommand = (string)commandHistory[--currentPosn];
            return lastCommand;
        }

        internal string GetNextCommand()
        {
            lastCommand = (string)commandHistory[++currentPosn];
            return LastCommand;
        }

        internal string LastCommand
        {
            get { return lastCommand; }
        }

        internal string[] GetCommandHistory()
        {
            return (string[])commandHistory.ToArray(typeof(string));
        }
    }

    /// <summary>
    /// Command argument class.
    /// </summary>
    public class CommandEnteredEventArgs : EventArgs
    {
        private string command;

        public CommandEnteredEventArgs(string command)
        {
            this.command = command;
        }

        public string Command
        {
            get { return command; }
        }
    }

    public class CompletionRequestedEventArgs : EventArgs
    {
        private string _uncompleted;
        private string _completed;

        public CompletionRequestedEventArgs(string uncompleted)
        {
            _uncompleted = uncompleted;
            _completed = null;
        }

        public string Uncompleted
        {
            get { return _uncompleted; }
        }

        public string Completed
        {
            get { return _completed; }
            set { _completed = value; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Converts
    {
        /// <summary>
        /// Custom MessageBox call. Excepts some random objects from IronPython and converts to string.
        /// </summary>
        /// <param name="inobject">Output object from IronPython.</param>
        public static void MessageBoxIronPy(Object inobject)
        {
            Type itstype = inobject.GetType();

            switch (itstype.FullName)
            {
                case "System.Int32":
                    MessageBox.Show(Convert.ToString(inobject));
                    break;
                case "System.Collections.Specialized.StringCollection":
                    StringCollection IPSC = (StringCollection)inobject;
                    StringEnumerator SCE = IPSC.GetEnumerator();
                    string output = "";
                    while (SCE.MoveNext())
                        output += SCE.Current.ToString();
                    MessageBox.Show(output);
                    break;
                default:
                    MessageBox.Show(inobject.ToString());
                    break;
            }
        }
    }

    public delegate void EventCommandEntered(object sender, CommandEnteredEventArgs e);

    public class PyConsole : FastColoredTextBox
    {
        #region Variables

        private string prompt = ">>>";
        private CommandHistory commandHistory = new CommandHistory();
        private StringBuilder defStmtBuilder = new StringBuilder();

        /// <summary>
        /// StringCollection of all MiscDirs
        /// </summary>
        public static StringCollection scMisc = new StringCollection();

        /// <summary>
        /// StringCollection of all Python24Dirs
        /// </summary>
        public static StringCollection scPython24 = new StringCollection();

        /// <summary>
        /// StringCollection of all IronPythonDirs
        /// </summary>
        public static StringCollection scIronPython = new StringCollection();

        /// <summary>
        /// Intellisense ToolTip.
        /// </summary>
        private readonly ToolTip intellisense = new ToolTip();

        /// <summary>
        /// A callback function that returns a completed version of
        /// the string passed into it.
        /// </summary>
        private readonly Func<string, string> _completionCallback;

        /// <summary>
        /// True if currently processing raw_text()
        /// </summary>
        public static Boolean IsRawInput;

        /// <summary>
        /// Hold raw_input prompt by user
        /// </summary>
        public string Rawprompt = "";

        #endregion

        /// <summary>
        /// Sends the prompt to the IronTextBox
        /// </summary>
        public void PrintPrompt()
        {
            string currentText = Text;

            //add newline if it does not exist
            if ((currentText.Length != 0) && (currentText[currentText.Length - 1] != '\n'))
                PrintLine();

            //add the prompt
            AddText(prompt);
        }

        /// <summary>
        /// Sends a newline character to the IronTextBox
        /// </summary>
        public void PrintLine()
        {
            AddText(Environment.NewLine);
        }

        /// <summary>
        /// Returns currentline's text string
        /// </summary>
        /// <returns>Returns currentline's text string</returns>
        public string GetTextAtPrompt()
        {
            if (GetCurrentLine() != "")
            {
                return GetCurrentLine().Substring(prompt.Length);
            }
            var mystring = Lines[Lines.Count - 2];
            return mystring.Substring(prompt.Length);
        }

        /// <summary>
        /// Replaces currentline's text string
        /// </summary>
        public void SetTextAtPrompt(string text)
        {
            var textWithPrompt = prompt + text;
            Lines[Lines.Count - 1] = textWithPrompt;
        }

        /// <summary>
        /// Add a command to IronTextBox command history.
        /// </summary>
        /// <param name="currentCommand">IronTextBox command line</param>
        public void AddcommandHistory(string currentCommand)
        {
            commandHistory.Add(currentCommand);
        }

        /// <summary>
        /// Returns true if Keys.Enter
        /// </summary>
        /// <param name="key">Keys</param>
        /// <returns>Returns true if Keys.Enter</returns>
        private static bool IsTerminatorKey(Keys key)
        {
            return key == Keys.Enter;
        }

        /// <summary>
        /// Returns true if (char)13 '\r'
        /// </summary>
        /// <param name="keyChar">char of keypressed</param>
        /// <returns>Returns true if (char)13 '\r'</returns>
        private static bool IsTerminatorKey(char keyChar)
        {
            return keyChar == 13;
        }

        /// <summary>
        /// Returns the current line, including prompt.
        /// </summary>
        /// <returns>Returns the current line, including prompt.</returns>
        private string GetCurrentLine()
        {
            if (Lines.Count > 0)
            {
                return Lines[Selection.Start.iLine];
            }
            return "";
        }

        /// <summary>
        /// Replaces the text at the current prompt.
        /// </summary>
        /// <param name="text">new text to replace old text.</param>
        private void ReplaceTextAtPrompt(string text)
        {
            string currentLine = GetCurrentLine();
            int charactersAfterPrompt = currentLine.Length - prompt.Length;

            if (charactersAfterPrompt == 0)
                AddText(text);
            else
            {
                Select(Text.Length - charactersAfterPrompt, charactersAfterPrompt);
                SelectedText = text;
            }
        }

        /// <summary>
        /// Returns true if caret is positioned on the currentline.
        /// </summary>
        /// <returns>Returns true if caret is positioned on the currentline.</returns>
        private bool IsCaretAtCurrentLine()
        {
            return TextLength - SelectionStart <= GetCurrentLine().Length;
        }

        /// <summary>
        /// Adds text to the IronTextBox
        /// </summary>
        /// <param name="text">text to be added</param>
        private void AddText(string text)
        {
            //Optional////////////
            scollection.Add(text); //Optional
            //this.Text = StringCollectionTostring(scollection); //Optional
            //////////////////////

            Enabled = false;
            Text += text;
            MoveCaretToEndOfText();
            Enabled = true;
            Focus();
            Update();
        }

        /// <summary>
        /// Returns a string retrieved from a StringCollection.
        /// </summary>
        /// <param name="inCol">StringCollection to be searched.</param>
        public string StringCollectionTostring(StringCollection inCol)
        {
            string value = "";
            var myEnumerator = inCol.GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                value += myEnumerator.Current;
            }

            return value;
        }

        /// <summary>
        /// Move caret to the end of the current text.
        /// </summary>
        private void MoveCaretToEndOfText()
        {
            GoEnd();
        }

        /// <summary>
        /// Returns true is the caret is just before the current prompt.
        /// </summary>
        /// <returns></returns>
        private bool IsCaretJustBeforePrompt()
        {
            return IsCaretAtCurrentLine() && GetCurrentCaretColumnPosition() == prompt.Length;
        }

        /// <summary>
        /// Returns the column position. Useful for selections.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentCaretColumnPosition()
        {
            string currentLine = GetCurrentLine();
            int currentCaretPosition = SelectionStart;
            return (currentCaretPosition - Text.Length + currentLine.Length);
        }

        /// <summary>
        /// Is the caret at a writable position.
        /// </summary>
        /// <returns></returns>
        private bool IsCaretAtWritablePosition()
        {
            return IsCaretAtCurrentLine() && GetCurrentCaretColumnPosition() >= prompt.Length;
        }

        /// <summary>
        /// Sets the text of the prompt.  Default is ">>>"
        /// </summary>
        /// <param name="val">string of new prompt</param>
        public void SetPromptText(string val)
        {
            GetCurrentLine();
            Select(0, prompt.Length);
            SelectedText = val;
            prompt = val;
        }

        /// <summary>
        /// Gets and sets the IronTextBox prompt.
        /// </summary>
        public string Prompt
        {
            get { return prompt; }
            set { SetPromptText(value); }
        }

        /// <summary>
        /// Returns the string array of the command history. 
        /// </summary>
        /// <returns></returns>
        public string[] GetCommandHistory()
        {
            return commandHistory.GetCommandHistory();
        }

        /// <summary>
        /// Adds text to the IronTextBox.
        /// </summary>
        /// <param name="text"></param>
        public void WriteText(string text)
        {
            AddText(text);
        }

    }
}

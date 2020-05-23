using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using Tao.Platform.Windows;

namespace PreprocessorLib
{
    public class UndoRedo
    {
        Stack<MemoryStream> Undo;
        Stack<MemoryStream> Redo;
        MemoryStream currentState, lastSaved;
        ProjectForm client;
        int stackCapacity;
        bool whileNavigate = false;

        public UndoRedo(ProjectForm client, int stackSize)
        {
            this.client = client;
            currentState = lastSaved = null;
            stackCapacity = stackSize;
            Undo = new Stack<MemoryStream>(stackSize);
            Redo = new Stack<MemoryStream>(stackSize);
            List<string> lst = new List<string>();
        }

        public void CheckForChanges()
        {
            if (currentState == null) currentState = client.getModelStream();
            else
            {
                MemoryStream currentModel = client.getModelStream();
                if (!currentState.GetBuffer().SequenceEqual(currentModel.GetBuffer()))
                {
                    pushToStack(ref Undo, currentState);
                    whileNavigate = false;
                    Redo.Clear();
                    currentState = currentModel;
                }
            }

        }

        public void UndoCommand()
        {
            if (Undo.Count > 0)
            {
                pushToStack(ref Redo, currentState);
                MemoryStream prevState = Undo.Pop();
                client.writeModelFromStream(prevState);
                currentState = prevState;
                whileNavigate = true;
            }
        }

        public void RedoCommand()
        {
            if (Redo.Count > 0 && whileNavigate)
            {
                pushToStack(ref Undo, currentState);
                MemoryStream nextState = Redo.Pop();
                client.writeModelFromStream(nextState);
                currentState = nextState;
            }
        }

        private void pushToStack(ref Stack<MemoryStream> stack, MemoryStream state)
        {
            if (stack.Count == stackCapacity)
            {
                stack = new Stack<MemoryStream>(stack.ToArray());
                stack.Pop();
                stack = new Stack<MemoryStream>(stack.ToArray());
            }
            stack.Push(state);
        }

        public void updateLastSaved()
        {
            lastSaved = client.getModelStream();
        }

        public bool modelDiffersFromSaved()
        {
            if (currentState == null) return false;
            if (lastSaved.GetBuffer().SequenceEqual(currentState.GetBuffer()))
                return false;
            else
                return true;
        }
    }
}

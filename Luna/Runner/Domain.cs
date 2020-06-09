﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luna.Runner;
using Luna.Assets;
using Luna.Types;

namespace Luna.Runner {
    class Domain {
        public double Scope = 0;
        public LInstance Instance = null;
        public Dictionary<string, LValue> Locals;
        public Stack<LValue> Stack = new Stack<LValue>();
        public Int32 ProgramCounter = 0;
        public bool ArrayNext = false;

        public Domain(LInstance _inst) {
            this.Scope = _inst.ID;
            this.Instance = _inst;
            this.Locals = new Dictionary<string, LValue>();
        }

        public Domain(LInstance _inst, Domain _other) {
            this.Scope = _inst.ID;
            this.Instance = _inst;
            this.Locals = _other.Locals;
        }

        public void ExecuteCode(Game _assets, LCode _code) {
            Int32 _programLength = _code.Instructions.Count;
            for (this.ProgramCounter = 0; this.ProgramCounter < _programLength; this.ProgramCounter++) {
                _code.Instructions[this.ProgramCounter].Perform(_assets, this, _code, this.Stack);
            }

#if (DEBUG)
            if (_assets.Headless == true) {
                Console.WriteLine("Instance Variables (Object: {0}, Count: {1})", this.Instance.Object.Name, this.Instance.Variables.Count);
                foreach (KeyValuePair<string, LValue> _instVar in this.Instance.Variables) {
                    Console.WriteLine("{0}.{1} = {2}", this.Instance.ID, _instVar.Key, _instVar.Value.Value);
                }
            }
#endif
        }
    }
}

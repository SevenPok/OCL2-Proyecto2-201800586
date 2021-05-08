using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OCL2_Proyecto2_201800586.Arbol
{
    public enum Natives
    {
        concat = 0,
        concat_str_bool = 1,
        concat_bool_str = 2,
        print_str = 3,
        cmp_str = 4,
        concat_num_str = 5,
        concat_str_num = 6,
    }
    class Generator
    {
        private static Generator generator;
        private int temporal;
        private int label;
        private LinkedList<String> code;
        private LinkedList<String> temps;
        private LinkedList<String> tempStorage;
        private LinkedList<String> functions;
        public String isFunc;
        public bool[] native;
        public Generator()
        {
            this.temporal = this.label = 0;
            this.temps = new LinkedList<String>();
            this.code = new LinkedList<String>();
            this.tempStorage = new LinkedList<String>();
            this.functions = new LinkedList<string>();
            this.native = new bool[] { false, false, false, false, false, false, false };
        }

        public static Generator getInstance()
        {
            if (generator == null)
            {
                generator = new Generator();
            }
            return generator;
        }

        public LinkedList<String> saveCode()
        {
            return this.code;
        }

        public void clearPrevious()
        {
            this.code = new LinkedList<String>();
        }

        public void addBegin(string id) {
            this.code.AddLast("\nvoid " + id + "(){");
        }

        public void addEnd()
        {
            this.code.AddLast("return;\n}\n");
        }

        public void addFunction()
        {
            foreach (String s in code)
            {
                functions.AddLast(s);
            }
        }

        public String  getFunctions()
        {
            String value = getNative();
            foreach(String s in functions)
            {
                value += s + "\n";
            }
            return value;
        }



        public void setCode(LinkedList<String> code)
        {
            this.code = code;
        }

        public LinkedList<String> getTempStorage()
        {
            return this.tempStorage;
        }

        public void clearTempStorage()
        {
            this.tempStorage = new LinkedList<String>();
        }

        public void setTempStorage(LinkedList<String> temp)
        {
            this.tempStorage = temp;
        }

        public void clearCode()
        {
            this.temporal = this.label = 0;
            this.code = new LinkedList<String>();
            this.temps = new LinkedList<String>();
            this.tempStorage = new LinkedList<String>();
            this.native = new bool[] { false, false, false, false, false, false, false };
        }
        public void addComment(string comment)
        {
            this.code.AddLast("/*****" + comment + "*****/");
        }

        public void addCode(String code)
        {
            this.code.AddLast(this.isFunc + code);
        }

        public String getHeader()
        {
            return "#include <stdio.h>\ndouble heap[30101999];\ndouble stack[30101999];\nint H = 0;\nint P = 0;\ndouble N0,N1,N2,N3,N4,N5,N6,N7;\n" + this.getTempsString() + "\n";
        }

        public String getTempsString()
        {
            String ret = "";

            if (this.temps.Count > 0)
                ret += "double ";
            else
                return "";
            for (int i = 0; i < this.temps.Count; i++)
            {
                String temp = this.temps.ElementAt(i);
                ret += temp;
                if (i < this.temps.Count - 1)
                {
                    ret += ",";
                }
            }
            ret += ";";
            return ret;
        }

        public void freeTemp(string temp)
        {
            foreach(string t in tempStorage)
            {
                if (temp == t)
                {
                    tempStorage.Remove(temp);
                    break;
                }
            }
        }

        public int saveTemps(Entorno enviorement) {
            if (this.tempStorage.Count > 0) {
                String temp = this.newTemp(); this.freeTemp(temp);
                int size = 0;

                this.addComment("BEGIN Saving temps");
                this.AddExp(temp, "P", enviorement.size.ToString(), "+");
                foreach (String s in tempStorage)
                {
                    size++;
                    this.addSetStack("(int)" + temp, s);
                    if (size != this.tempStorage.Count)
                        this.AddExp(temp, temp, "1", "+");
                }
                this.addComment("END Saving temps");
            }
            int ptr = enviorement.size;
            enviorement.size = ptr + this.tempStorage.Count;
            return ptr;
        }

        public void recoverTemps(Entorno enviorement, int pos)
        {
            if (this.tempStorage.Count > 0)
            {
                String temp = this.newTemp(); this.freeTemp(temp);
                int size = 0;

                this.addComment("BEGIN Recovering temps");
                this.AddExp(temp, "P", pos.ToString(), "+");

                foreach(string value in tempStorage)
                {
                    size++;
                    this.addGetStack(value, temp);
                    if (size != this.tempStorage.Count)
                        this.AddExp(temp, temp, "1", "+");
                }

                this.addComment("END Recovering temps");
                enviorement.size = pos;
            }
        }

        public void addTemp(string temp)
        {
            if (!this.tempStorage.Contains(temp))
                this.tempStorage.AddLast(temp);
        }

        public String getCode()
        {
            String ret = this.getHeader();

            foreach (String line in this.code)
            {
                ret += "\t" + line + "\n";
            }
          
            return ret;
        }

        public string getNative()
        {
            String ret = "";
            for (int i = 0; i < native.Length; i++)
            {
                if (native[i])
                {
                    switch (i)
                    {
                        case 0:
                            ret += native_concat();
                            break;
                        case 1:

                            break;
                        case 2:

                            break;
                        case 3:
                            ret += native_print_str();
                            break;
                        case 4:

                            break;
                        case 5:

                            break;
                        case 6:

                            break;
                    }
                }
            }
            return ret;
        }
        public void addSpace()
        {
            this.code.AddLast("\n");
        }

        public String newTemp()
        {
            String temp = "t" + this.temporal++;
            this.tempStorage.AddLast(temp);
            this.temps.AddLast(temp);
            return temp;
        }

        public String newLabel()
        {
            return "L" + this.label++;
        }

        public void addLabel(String label)
        {
            this.code.AddLast(this.isFunc + label + ":");
        }

        public void AddExp(String target, String left, String right = "", String op = "")
        {
            this.code.AddLast(this.isFunc + target + " = " + left + op + right + ";");
        }

        public void addGoto(String label)
        {
            this.code.AddLast(this.isFunc + "goto " + label + ";");
        }

        public void addIf(String left, String right, String op, String label)
        {
            this.code.AddLast(this.isFunc + "if (" + left + op + right + ") goto " + label + ";");
        }

        // TODO:
        public void addPrint(String format, String value)
        {
            this.code.AddLast(this.isFunc + "printf(\"%" + format + "\", " + value + ");");
        }

        public void nextHeap()
        {
            this.code.AddLast(this.isFunc + "H = H + 1;");
        }

        public void addGetHeap(String target, String index)
        {
            this.code.AddLast(this.isFunc + target + " = heap[" + index + "];");
        }

        public void addSetHeap(String index, String value)
        {
            this.code.AddLast(this.isFunc + "heap[" + index + "] = " + value + ";");
        }

        public void addGetStack(String target, String index)
        {
            this.code.AddLast(this.isFunc + target + " = stack[(int)" + index + "];");
        }

        public void addSetStack(String index, String value)
        {
            this.code.AddLast(this.isFunc + "stack[" + index + "] = " + value + ";");
        }

        public void addNextEnv(String size)
        {
            this.code.AddLast(this.isFunc + "P = P + " + size + ";");
        }

        public void addAntEnv(String size)
        {
            this.code.AddLast(this.isFunc + "P = P - " + size + ";");
        }

        public void addCallFunc(String id)
        {
            this.code.AddLast(id + "();");
        }

        public void addBeginFunc(String id)
        {
            if (this.isFunc == "")
            {
                this.isFunc = "\t";
                this.code.AddLast("void " + id + "(){");
            }
        }

        public void addEndFunc()
        {
            if (this.isFunc != "")
            {
                this.isFunc = "";
                this.code.AddLast("return;\n}");
            }
        }

        public void printTrue()
        {
            this.addPrint("c", "(char)116");
            this.addPrint("c", "(char)114");
            this.addPrint("c", "(char)117");
            this.addPrint("c", "(char)101");
        }

        public void printFalse()
        {
            this.addPrint("c", "(char)102");
            this.addPrint("c", "(char)97");
            this.addPrint("c", "(char)108");
            this.addPrint("c", "(char)115");
            this.addPrint("c", "(char)101");
        }

        public void addNative(Natives n)
        {
            native[n.GetHashCode()] = true;
        }

        public String native_print_str()
        {
            return "\n" + @"void native_print_str(){
        N0 = P + 0;
        N1 = stack[(int)N0];
        N2 = heap[(int)N1];
    L0:
        if (N2 != -1)
        goto L1;
        goto L2;
    L1:
        if (N2 != 92)
        goto L3;
        goto L4;
    L4:
        N3 = N1 + 1;
        N2 = heap[(int)N3];
        if (N2 == -1)
        goto L2;
        goto L5;
    L5:
        if (N2 == 92)
        goto L6;
        goto L7;
    L7:
        if (N2 == 34)
        goto L8;
        goto L9;
    L9:
        if (N2 == 110)
        goto L10;
        goto L11;
    L11:
        if (N2 == 114)
        goto L12;
        goto L13;
    L13:
        if (N2 == 116)
        goto L14;
        goto L15;
    L15:
        N2 = heap[(int)N1];
        goto L3;
    L6:
        N1 = N1 + 1;
        N2 = 92;
        goto L3;
    L8:
        N1 = N1 + 1;
        N2 = 34;
        goto L3;
    L10:
        N1 = N1 + 1;
        N2 = 10;
        goto L3;
    L12:
        N1 = N1 + 1;
        N2 = 10;
        goto L3;
    L14:
        N1 = N1 + 1;
        N2 = 9;
        goto L3;
    L3:
        printf(" + "\"" + "%c" + "\"" + @", (int)N2);
        N1 = N1 + 1;
        N2 = heap[(int)N1];
        goto L0;
    L2:
        return;" + "\n" +
    "}" + "\n";
        }

        public string native_concat()  {
            return "\n" + @"void native_concat(){
        N0 = P + 1;
        N1 = stack[(int)N0];
        N2 = heap[(int)N1];
        N3 = H;
    L0:
        if (N2 != -1)
            goto L1;
        goto L2;
    L1:
        heap[(int)H] = N2;
        H = H + 1;
        N1 = N1 + 1;
        N2 = heap[(int)N1];
        goto L0;
    L2:
        N0 = P + 2;
        N1 = stack[(int)N0];
        N2 = heap[(int)N1];
    L3:
        if (N2 != -1)
            goto L4;
        goto L5;
    L4:
        heap[(int)H] = N2;
        H = H + 1;
        N1 = N1 + 1;
        N2 = heap[(int)N1];
        goto L3;
    L5:
        heap[(int)H] = -1;
        H = H + 1;
        stack[(int)P] = N3;
        return;" + "\n" +
    "}" + "\n";
        }
    }
}

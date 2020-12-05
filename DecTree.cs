﻿using System;
using System.IO;
using System.Text.Encodings;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Dec
{
    [DataContractAttribute]
    public class DecTree<E>
    {
        [DataMemberAttribute]
        public DecTree<E> dt0;
        [DataMemberAttribute]
        public DecTree<E> dt1;
        [DataMemberAttribute]
        public DecTree<E> dt2;
        [DataMemberAttribute]
        public DecTree<E> dt3;
        [DataMemberAttribute]
        public DecTree<E> dt4;
        [DataMemberAttribute]
        public DecTree<E> dt5;
        [DataMemberAttribute]
        public DecTree<E> dt6;
        [DataMemberAttribute]
        public DecTree<E> dt7;
        [DataMemberAttribute]
        public DecTree<E> dt8;
        [DataMemberAttribute]
        public DecTree<E> dt9;
        [DataMemberAttribute]
        public DecTree<E> dtC;
        [DataMemberAttribute]
        public DecTree<E> dtN;
        [IgnoreDataMemberAttribute]
        public DecTree<E> dtP;
        [DataMemberAttribute]
        public DecTree<long> primes;
        [DataMemberAttribute]
        public E element = default(E);

        [DataMemberAttribute]
        long type = 0;
        [DataMemberAttribute]
        long counter = 0;
        [DataMemberAttribute]
        long serializationCounter = 0;

        static int ZERO = 0;
        static int ONE = 1;
        static int TWO = 2;
        static int THREE = 3;
        static int FOUR = 4;
        static int FIVE = 5;
        static int SIX = 6;
        static int SEVEN = 7;
        static int EIGHT = 8;
        static int NINE = 9;
        static int NEGATIVE = -1;
        static int CHILD = -3;
        static int PARENT = -100;

        /* used to Make Type-Specific DecTrees*/
        public DecTree()
        {
        }
        private DecTree(int type)
        {
            this.type = type;
        }

        public DecTree(E element)
        {
            this.element = element;
        }

        public void counterDecrement()
        {
            this.counter--;
            if (this.dtP != null)
            {
                this.dtP.counterDecrement();
            }
        }

        /// <summary>
        /// Trying to figure out how not to break the hasher with this. Unused for now.
        /// </summary>
        public void counterIncrement()
        {
            this.counter++;
            if (this.dtP != null)
            {
                this.dtP.counterIncrement();
            }
        }

        public DecTree<E> MakeDecTree(int type)
        {
            DecTree<E> opTree;
            switch (type)
            {
                case 0:
                    {
                        this.dt0 ??= new DecTree<E>(ZERO);
                        opTree = this.dt0;
                        break;
                    }

                case 1:
                    {
                        this.dt1 ??= new DecTree<E>(ONE);
                        opTree = this.dt1;
                        break;
                    }

                case 2:
                    {
                        this.dt2 ??= new DecTree<E>(TWO);
                        opTree = this.dt2;
                        break;
                    }

                case 3:
                    {
                        this.dt3 ??= new DecTree<E>(THREE);
                        opTree = this.dt3;
                        break;
                    }
                case 4:
                    {
                        this.dt4 ??= new DecTree<E>(FOUR);
                        opTree = this.dt4;
                        break;
                    }

                case 5:
                    {
                        this.dt5 ??= new DecTree<E>(FIVE);
                        opTree = this.dt5;
                        break;
                    }

                case 6:
                    {
                        this.dt6 ??= new DecTree<E>(SIX);
                        opTree = this.dt6;
                        break;
                    }

                case 7:
                    {
                        this.dt7 ??= new DecTree<E>(SEVEN);
                        opTree = this.dt7;
                        break;
                    }
                case 8:
                    {
                        this.dt8 ??= new DecTree<E>(EIGHT);
                        opTree = this.dt8;
                        break;
                    }

                case 9:
                    {
                        this.dt9 ??= new DecTree<E>(NINE);
                        opTree = this.dt9;
                        break;
                    }

                case -1:
                    {
                        this.dtN ??= new DecTree<E>(NEGATIVE);
                        opTree = this.dtN;
                        break;
                    }

                case -3:
                    {
                        this.dtC ??= new DecTree<E>(CHILD);
                        opTree = this.dtC;
                        break;
                    }

                case -100:
                    {
                        this.dtP ??= new DecTree<E>(PARENT);
                        opTree = this.dtP;
                        break;
                    }

                default:
                    opTree = null;
                    break;

            }
            opTree.dtP = this;
            return opTree;
        }

        public DecTree<E> GetDecTree(int type)
        {
            DecTree<E> opTree;

            switch (type)
            {
                case 0:
                    {
                        opTree = this.dt0;
                        break;
                    }

                case 1:
                    {
                        opTree = this.dt1;
                        break;
                    }

                case 2:
                    {
                        opTree = this.dt2;
                        break;
                    }

                case 3:
                    {
                        opTree = this.dt3;
                        break;
                    }
                case 4:
                    {
                        opTree = this.dt4;
                        break;
                    }

                case 5:
                    {
                        opTree = this.dt5;
                        break;
                    }

                case 6:
                    {
                        opTree = this.dt6;
                        break;
                    }

                case 7:
                    {
                        opTree = this.dt7;
                        break;
                    }
                case 8:
                    {
                        opTree = this.dt8;
                        break;
                    }

                case 9:
                    {
                        opTree = this.dt9;
                        break;
                    }

                case -1:
                    {
                        opTree = this.dtN;
                        break;
                    }

                case -3:
                    {
                        opTree = this.dtC;
                        break;
                    }
                case -100:
                    {
                        opTree = this.dtP;
                        break;
                    }

                default:
                    opTree = null;
                    break;

            }
            return opTree;
        }
        public DecTree<E> PaveTo(long address)
        {
            DecTree<E> retTree = this;
            if (address == 0)
            {
                return this;
            }
            if (address < 0)
            {
                retTree = retTree.MakeDecTree(NEGATIVE);
                address *= -1;
            }
            while ((int)address > 0)
            // TODO: I kind of want to parse left to right but I don't want to sacrifice the runtime.
            {
                int mod = (int)address % 10;
                address /= 10;
                retTree = retTree.MakeDecTree(mod);
            }
            return retTree;
        }

        public DecTree<E> PaveTo(String address)
        {
            DecTree<E> retTree = this;

            long longAddress = Hasher(0, address);

            if (longAddress == 0)
            {
                return this;
            }
            if (longAddress < 0)
            {
                retTree = retTree.MakeDecTree(NEGATIVE);
                longAddress *= -1;
            }
            while ((int)longAddress > 0)
            {
                int mod = (int)longAddress % 10;
                longAddress /= 10;
                retTree = retTree.MakeDecTree(mod);
            }
            return retTree;
        }

        public DecTree<E> GoGet(long address)
        {
            DecTree<E> retTree = this;
            DecTree<E> opTree = retTree;

            if (address == 0)
            {
                return this;
            }
            else if (address < 0)
            {
                opTree = opTree.GetDecTree(NEGATIVE);
                address *= -1;
            }
            while (address > 0)
            {
                int mod = (int)address % 10;
                address /= 10;
                opTree = opTree.GetDecTree(mod);
            }
            return opTree;
        }

        public DecTree<E> GoGet(String address)
        {
            DecTree<E> retTree = this;
            DecTree<E> opTree = retTree;

            long longAddress = Hasher(0, address);

            if (longAddress == 0)
            {
                return this;
            }
            else if (longAddress < 0)
            {
                opTree = opTree.GetDecTree(NEGATIVE);
                longAddress *= -1;
            }
            else while (longAddress > 0)
                {
                    int mod = (int)longAddress % 10;
                    longAddress /= 10;
                    opTree = opTree.GetDecTree(mod);
                }
            return opTree;
        }

        public DecTree<E> Add(E element)
        {
            DecTree<E> opTree = this;
            if (opTree.dtC == null)
            {
                opTree = opTree.MakeDecTree(CHILD);
                opTree.element = element;
                return opTree;
            }
            else
            {
                DecTree<E> nextInChain = opTree.dtC;
                opTree.dtC = default(DecTree<E>);
                opTree = opTree.MakeDecTree(CHILD);
                opTree.element = element;
                opTree.dtC = nextInChain;
                nextInChain.dtP = opTree;
                return opTree;
            }
        }

        public DecTree<E> Add(long address, E element)
        {
            return this.PaveTo(address).Add(element);
        }

        public DecTree<E> Add(string address, E element)
        {
            return this.PaveTo(Hasher(0, address)).Add(element);
        }

        //<>Section<>

        public DecTree<E> AddL(E element)
        {
            DecTree<E> opTree = this;
            while (opTree.dtC != null)
            {
                opTree = opTree.dtC;
            }
            opTree = opTree.Add(element);
            return opTree;
        }

        public DecTree<E> AddL(long address, E element)
        {
            DecTree<E> opTree = this.PaveTo(address);
            return opTree.AddL(element);
        }
        public DecTree<E> AddL(string address, E element)
        {
            DecTree<E> opTree = this.PaveTo(Hasher(0, address));
            return opTree.AddL(element);
        }

        public E Get()
        {
            return this.dtC.element;
        }

        public DecTree<E> TGet(String address)
        {
            return this.GoGet(address).dtC;
        }

        public E Get(long address)
        {
            return this.GoGet(address).Get();
        }

        public E Get(string address)
        {
            return this.GoGet(Hasher(0, address)).Get();
        }

        public E GetL(long address)
        {
            DecTree<E> opTree = this.PaveTo(address);
            while (opTree.dtC != null)
            {
                opTree = opTree.dtC;
            }
            return opTree.element;
        }

        public E GetL(string address)
        {
            DecTree<E> opTree = this.PaveTo(Hasher(0, address));
            while (opTree.dtC != null)
            {
                opTree = opTree.dtC;
            }
            return opTree.element;
        }

        public DecTree<E> TGet(long address)
        {
            return this.PaveTo(address).dtC;
        }

        public bool Rm()
        {
            if (this.dtC != null && this.dtC.dtC != null)
            {
                this.dtC = this.dtC.dtC;
                return true;
            }
            else if (this.dtC != null && this.dtC.dtC == null)
            {
                this.dtC = null;
                this.element = default(E);
            }
            {
                return false;
            }
        }

        public bool Rm(long address)
        {
            return this.GoGet(address).Rm();
        }

        public bool Rm(String address)
        {
            return this.Rm(Hasher(0, address));
        }

        public bool RmL()
        {
            DecTree<E> FocusTree = this;
            DecTree<E> ChildTree;
            if (FocusTree.dtC == null)
            {
                return false;
            }
            ChildTree = FocusTree.dtC;
            while (ChildTree != null)
            {
                ChildTree = ChildTree.dtC;
                FocusTree = FocusTree.dtC;
            }
            FocusTree.dtC = null;
            counterDecrement();
            return true;
        }

        public bool RmL(long address)
        {
            return this.PaveTo(address).RmL(address);
        }



        public bool RmL(string address)
        {
            return this.Rm(int.Parse(address));
        }


        public long Hasher(long zero, string tag)
        {
            long hashValue = zero;
            for (int i = 0; i < tag.Length; i++)
            {
                hashValue += tag[i] * Prime(i);
            }
            return hashValue;
        }

        // Level 0
        public long Prime(long index)
        {
            //only want to add once.
            if (primes == null)
            {
                primes = new DecTree<long>();
                primes.Add(0, 2);
            }

            //can see how many times this method has been this.primes with primes.TGet(0) analysis.
            for (long i = primes.Get(primes.counter) + 1; i < primes.Get(primes.counter) * 2; i++)
            { // if(Bertrand's Postulate) {Infinite Loop}.
                if (primes.counter == index)
                {
                    break;
                }
                for (long j = 0; j <= primes.counter; j++)
                {
                    if (primes.counter >= index)
                    {
                        break;
                    }
                    if (i % primes.Get(j) == 0)
                    {
                        break;
                    }
                    else
                    {
                        if (j == primes.counter)
                        {
                            primes.Add(++primes.counter, i);
                        }
                    }
                    // LOOP ENDS HERE
                }
            }
            return primes.Get(index);
        }

        private DecTree<DecTree<E>> CollectSubTrees(DecTree<DecTree<E>> referenceHolder)
        {
            DecTree<E>[] order = new DecTree<E>[] { dt0, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dtC, dtN };
            foreach (DecTree<E> member in order)
            {
                if (member != null)
                {
                    referenceHolder.Add(++referenceHolder.counter, member);
                    member.serializationCounter = referenceHolder.counter;
                    member.CollectSubTrees(referenceHolder);
                }
            }
            return referenceHolder;
        }

        /// <summary>
        /// OUTPUT FORMAT:
        /// $"[{DecTree Index}] : <{element.ToString()}>\n"
        /// $"\t{Child Index}: {Index of child tree}\n")
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        //If you make changes, ensure that you amend FromString
        {
            DecString retString = new DecString();
            DecTree<E>[] order = new DecTree<E>[] { dt0, dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dtC, dtN };
            DecTree<DecTree<E>> referenceHolder = new DecTree<DecTree<E>>();
            referenceHolder.Add(0, this);
            foreach (DecTree<E> member in order)
            {
                if (member != null)
                {
                    referenceHolder.Add(++referenceHolder.counter, member);
                    member.serializationCounter = referenceHolder.counter;
                    member.CollectSubTrees(referenceHolder);
                }
            }
            for (int i = 0; i <= referenceHolder.counter; i++)
            {
                DecTree<E> currTree = referenceHolder.Get(i);
                if (currTree.element == null)
                {
                    retString.AddString($"{i}:\n");
                }
                else
                {
                    retString.AddString($"{i}:{currTree.element.ToString()}\n");
                }

                order[0] = currTree.dt0;
                order[1] = currTree.dt1;
                order[2] = currTree.dt2;
                order[3] = currTree.dt3;
                order[4] = currTree.dt4;
                order[5] = currTree.dt5;
                order[6] = currTree.dt6;
                order[7] = currTree.dt7;
                order[8] = currTree.dt8;
                order[9] = currTree.dt9;
                order[10] = currTree.dtC;
                order[11] = currTree.dtN;
                for (int j = 0; j < order.Length; j++)
                {
                    if (order[j] != null)
                    {
                        retString.AddString($"\t{j}:{order[j].serializationCounter}\n");
                    }
                }
            }
            return retString.ToString();
        }

        public void Export(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            string thisToString = this.ToString();
            byte[] thisToString_byteArray = new byte[thisToString.Length];
            for (int i = 0; i < thisToString.Length; i++)
            {
                thisToString_byteArray[i] = (byte)thisToString[i];
            }
            fs.Write(thisToString_byteArray, 0, thisToString.Length);
            fs.Dispose();
        }

        public static DecTree<string> Import(string path)
        {
            DecTree<DecTree<string>> holderTree = new DecTree<DecTree<string>>();

            StreamReader sr = new StreamReader(path);
            DecString ds = new DecString();
            string nextLine = sr.ReadLine();
            long index = -1;
            while (nextLine != null)
            {
                if (nextLine[0] == '\t')
                //First pass. Must read in all DecTrees before it makes sense to pass references to them.
                {
                    nextLine = sr.ReadLine();
                    continue;
                }
                else
                /*The only other case is for the line to begin with an index.
                The procedure is to read the index, read the element, store 
                a DecTree containing the element.*/
                {
                    int i = 0;
                    for (; nextLine[i] > 0x2f && nextLine[i] < 0x3a; i++)
                    {
                        ds.AddChar(nextLine[i]);
                    }
                    index = Int64.Parse(ds.ToString());
                    ds.Clear();
                    i++; //to avoid the colon
                    for (; i < nextLine.Length; i++)
                    {
                        ds.AddChar(nextLine[i]);
                    }
                    string element = ds.ToString();
                    ds.Clear();

                    holderTree.Add(index, new DecTree<string>(element));
                }
                nextLine = sr.ReadLine();
                //repeat for each line.
            }

            sr.Close();
            sr = new StreamReader(path);
            //close then reopen the file so we can read agan from beginning.
            nextLine = sr.ReadLine();
            DecTree<string> currDecTree;
            index = -1;
            while (nextLine != null)
            {
                if (nextLine[0] == '\t')
                /*here, we do the references.
                The approach is as follows:
                the index is already set from the else case.
                We get the tree with that index, find the identities
                of the subordinate DecTrees, and properly assign
                the references according to {0-9 : dt0-dt9, 10->dtC,
                11->dtN*/
                {
                    currDecTree = holderTree.Get(index);
                    if (nextLine[1] == '0')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt0 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                    else if (nextLine[1] == '1')
                    {
                        if (nextLine[2] == ':')
                        {
                            for (int i = 3; i < nextLine.Length; i++)
                            {
                                ds.AddChar(nextLine[i]);
                            }
                            currDecTree.dt1 = holderTree.Get(Int64.Parse(ds.ToString()));
                            ds.Clear();
                        }
                        if (nextLine[2] == '0')
                        {
                            for (int i = 4; i < nextLine.Length; i++)
                            {
                                ds.AddChar(nextLine[i]);
                            }
                            currDecTree.dtC = holderTree.Get(Int64.Parse(ds.ToString()));
                            ds.Clear();
                        }
                        else if (nextLine[2] == '1')
                        {
                            for (int i = 4; i < nextLine.Length; i++)
                            {
                                ds.AddChar(nextLine[i]);
                            }
                            currDecTree.dtN = holderTree.Get(Int64.Parse(ds.ToString()));
                            ds.Clear();
                        }
                    }
                    else if (nextLine[1] == '2')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt2 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                    else if (nextLine[1] == '3')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt3 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                    else if (nextLine[1] == '4')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt4 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                    else if (nextLine[1] == '5')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt5 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                    else if (nextLine[1] == '6')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt6 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                    else if (nextLine[1] == '7')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt7 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                    else if (nextLine[1] == '8')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt8 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                    else if (nextLine[1] == '9')
                    {
                        for (int i = 3; i < nextLine.Length; i++)
                        {
                            ds.AddChar(nextLine[i]);
                        }
                        currDecTree.dt9 = holderTree.Get(Int64.Parse(ds.ToString()));
                        ds.Clear();
                    }
                }
                else
                //parse for index, find it, store in index variable.
                {
                    int i = 0;
                    for (; nextLine[i] > 0x2f && nextLine[i] < 0x3a; i++)
                    {
                        ds.AddChar(nextLine[i]);
                    }
                    index = Int64.Parse(ds.ToString());
                    ds.Clear();
                }
                nextLine = sr.ReadLine();
            }
            sr.Dispose();
            return holderTree.Get(0);
            /*return *.Get(0) because the base tree has id 0.
            It's an axiom.*/
        }
    }
}
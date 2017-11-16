using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.WorkflowLib
{
    public delegate void PropertyEventHandler<TKey, TValue>(object sender, PropertyEventArgs<TKey, TValue> e);

    public class PropertyDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        internal event PropertyEventHandler<TKey, TValue> Adding;
        internal event PropertyEventHandler<TKey, TValue> Added;
        internal event PropertyEventHandler<TKey, TValue> Removing;
        internal event PropertyEventHandler<TKey, TValue> Removed;
        internal event PropertyEventHandler<TKey, TValue> Updating;
        internal event PropertyEventHandler<TKey, TValue> Updated;
        internal event PropertyEventHandler<TKey, TValue> Clearing;
        internal event PropertyEventHandler<TKey, TValue> Cleared;

        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                PropertyEventArgs<TKey, TValue> e =
                    new PropertyEventArgs<TKey, TValue>(key, value);

                if (!base.ContainsKey(key))
                {
                    this.Add(key, value);
                }
                else
                {
                    if (this.Updating != null)
                        this.Updating(this, e);

                    base[key] = value;

                    if (this.Updated != null)
                        this.Updated(this, e);
                }
            }
        }

        public new void Add(TKey key, TValue value)
        {
            PropertyEventArgs<TKey, TValue> e =
                new PropertyEventArgs<TKey, TValue>(key, value);

            if (this.Adding != null)
                this.Adding(this, e);

            base.Add(key, value);

            if (this.Added != null)
                this.Added(this, e);
        }

        public new void Remove(TKey key)
        {
            PropertyEventArgs<TKey, TValue> e =
                new PropertyEventArgs<TKey, TValue>(key, base[key]);

            if (this.Removing != null)
                this.Removing(this, e);

            base.Remove(key);

            if (this.Removed != null)
                this.Removed(this, e);
        }

        public new void Clear()
        {
            if (this.Clearing != null)
                this.Clearing(this, null);

            base.Clear();

            if (this.Cleared != null)
                this.Cleared(this, null);
        }
    }
}

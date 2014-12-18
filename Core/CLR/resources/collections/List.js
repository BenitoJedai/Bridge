Bridge.Class.generic('Bridge.List$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.List$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.ICollection$1(T)],
        $init: function (obj) {
            if (Object.prototype.toString.call(obj) === '[object Array]') {
                this.items = obj;
            }
            else if (Bridge.is(obj, Bridge.IEnumerable)) {
                this.items = Bridge.toArray(obj);
            }
            else {
                this.items = [];
            }
        },

        checkIndex: function (index) {
            if (index < 0 || index > (this.items.length - 1)) {
                throw new Error('Index out of range');
            }
        },

        getCount: function () {
            return this.items.length;
        },

        get: function (index) {
            this.checkIndex(index);

            return this.items[index];
        },

        set: function (index, value) {
            this.checkIndex(index);
            this.items[index] = value;
        },

        add: function (value) {
            this.items.push(value);
        },

        addRange: function (items) {
            var array = Bridge.toArray(items),
                i,
                len;

            for (i = 0, len = array.length; i < len; ++i) {
                this.items.push(array[i]);
            }
        },

        clear: function () {
            this.items = [];
        },

        indexOf: function (item, startIndex) {
            var i;

            if (!Bridge.isDefined(startIndex)) {
                startIndex = 0;
            }

            if (startIndex != 0) {
                this.checkIndex(index);
            }

            for (i = startIndex; i < this.items.length; i++) {
                if (item === this.items[i]) {
                    return i;
                }
            }

            return -1;
        },

        contains: function (item) {
            return this.indexOf(item) > -1;
        },

        getEnumerator: function () {
            return new Bridge.ArrayEnumerator(this.items);
        },

        getRange: function (index, count) {
            if (!Bridge.isDefined(index)) {
                index = 0;
            }

            if (!Bridge.isDefined(count)) {
                count = this.items.length;
            }

            if (index != 0) {
                this.checkIndex(index);
            }

            this.checkIndex(index + count - 1);

            var result = [],
                i;

            for (i = index; i < count; i++) {
                result.push(this.items[i]);
            }

            return result;
        },

        insert: function (index, item) {
            if (index != 0) {
                this.checkIndex(index);
            }


            if (Bridge.isArray(item)) {
                for (var i = 0; i < item.length; i++) {
                    this.insert(index++, item[i]);
                }
            }
            else {
                this.items.splice(index, 0, item);
            }
        },

        join: function (delimeter) {
            return this.items.join(delimeter);
        },

        lastIndexOf: function (item, fromIndex) {
            if (!Bridge.isDefined(fromIndex)) {
                fromIndex = this.items.length - 1;
            }

            if (fromIndex != 0) {
                this.checkIndex(fromIndex);
            }

            for (var i = fromIndex; i >= 0; i--) {
                if (item === this.items[i]) {
                    return i;
                }
            }

            return -1;
        },

        remove: function (item) {
            var index = this.indexOf(item);

            this.checkIndex(index);
            this.items.splice(index, 1);
        },

        removeAt: function (index) {
            this.checkIndex(index);
            this.items.splice(index, 1);
        },

        removeRange: function (index, count) {
            this.checkIndex(index);
            this.items.splice(index, count);
        },

        reverse: function () {
            this.items.reverse();
        },

        slice: function (start, end) {
            this.items.slice(start, end);
        },

        sort: function (comparison) {
            this.items.sort(comparison);
        },

        splice: function (start, count, items) {
            this.items.splice(start, count, items);
        },

        unshift: function () {
            this.items.unshift();
        },

        toArray: function () {
            return Bridge.toArray(this);
        }
    }));
});

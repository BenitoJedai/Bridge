ScriptKit.Class.extend("ScriptKit.ArrayIterator", {
    init: function (array) {
        this.array = array;
        this.index = 0;
    },

    hasNext : function () {
	    return this.index < this.array.length;
	},

    next : function() {
        return this.array[this.index++];
    }
});
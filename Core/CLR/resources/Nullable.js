
// @source resources/Core.js

Bridge.nullable = {
    hasValue: function (obj) {
        return (obj !== null) && (obj !== undefined);
    },

    getValue: function (obj) {
        if (!Bridge.nullable.hasValue(obj)) {
            throw Error("Nullable instance doesn't have a value.");
        }
        return obj;
    },

    getValueOrDefault: function (obj, defValue) {
        return Bridge.nullable.hasValue(obj) ? obj : defValue;
    }
};

Bridge.hasValue = Bridge.nullable.hasValue;
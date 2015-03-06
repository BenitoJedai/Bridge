Bridge.Class.extend('Bridge.IEnumerable', {});
Bridge.Class.extend('Bridge.IEnumerator', {});
Bridge.Class.extend('Bridge.IEqualityComparer', {});
Bridge.Class.extend('Bridge.ICollection', {
    $extend: [Bridge.IEnumerable]
});

Bridge.Class.generic('Bridge.IEnumerator$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IEnumerator$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEnumerator]
    }));
});

Bridge.Class.generic('Bridge.IEnumerable$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IEnumerable$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEnumerable]
    }));
});

Bridge.Class.generic('Bridge.ICollection$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.ICollection$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEnumerable$1(T)]
    }));
});

Bridge.Class.generic('Bridge.IEqualityComparer$1', function (T) {
    var $$name = Bridge.Class.genericName('Bridge.IEqualityComparer$1', T);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEqualityComparer]
    }));
});

Bridge.Class.generic('Bridge.IDictionary$2', function (TKey, TValue) {
    var $$name = Bridge.Class.genericName('Bridge.IDictionary$2', TKey, TValue);
    return Bridge.Class.cache[$$name] || (Bridge.Class.cache[$$name] = Bridge.Class.extend($$name, {
        $extend: [Bridge.IEnumerable$1(Bridge.KeyValuePair$2(TKey, TValue))],
    }));
});

Bridge.String = {
    format : function (format) {
        var _formatRe = /\{\{|\}\}|\{(\d+)(?:,(-?\d+))?(?:\:([\w\s\.]*))?\}/g,
            args = Array.prototype.slice.call(arguments, 1);

        return format.replace(_formatRe, function (m, idx, alignment, formatStr) {
            if (m === "{{" || m === "}}") {
                return m.charAt(0);
            }

            var replaceValue = args[parseInt(idx, 10)],
                values,
                match;

            if (!Bridge.isDefined(replaceValue, true)) {
                return "";
            }

            if (alignment) {
                alignment = parseInt(alignment, 10);
                if (!Bridge.isNumber(alignment)) {
                    alignment = null;
                }
            }

            if (formatStr && Bridge.is(replaceValue, Bridge.IFormattable)) {
                values = [replaceValue];

                return Bridge.String.alignString(Bridge.format(replaceValue, formatStr), alignment);
            }

            return Bridge.String.alignString(replaceValue.toString(), alignment);
        });
    },

    alignString : function (str, alignment, pad, dir) {
        if (!alignment)
        {
            return str;
        }

        if (!pad)
        {
            pad = " ";
        }

        if (!dir)
        {
            dir = alignment < 0 ? 2 : 1;
        }

        alignment = Math.abs(alignment);

        if (alignment + 1 >= str.length) {
            switch (dir) {
                case 2:
                    str = Array(alignment + 1 - str.length).join(pad) + str;
                    break;

                case 3:
                    var padlen = alignment - str.length,
                        right = Math.ceil(padlen / 2),
                        left = padlen - right;

                    str = Array(left + 1).join(pad) + str + Array(right + 1).join(pad);
                    break;

                case 1:
                default:
                    str = str + Array(alignment + 1 - str.length).join(pad);
                    break;
            }
        }

        return str;
    },

    startsWith: function (str, prefix) {
        if (!prefix.length) {
            return true;
        }
        if (prefix.length > str.length) {
            return false;
        }
        return str.match("^" + prefix) !== null;
    },

    endsWith: function (str, suffix) {
        if (!suffix.length) {
            return true;
        }
        if (suffix.length > str.length) {
            return false;
        }
        return str.match(suffix + "$") !== null;
    }
};
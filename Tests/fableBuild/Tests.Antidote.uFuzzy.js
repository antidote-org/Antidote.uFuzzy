import ufuzzy from "@leeoniya/ufuzzy";
import { map, toArray } from "./fable_modules/fable-library.3.7.8/Seq.js";
import { assertEqual } from "./fable_modules/fable-library.3.7.8/Util.js";

export const haystack = ["puzzle", "Super Awesome Thing (now with stuff!)", "FileName.js", "/feeding/the/catPic.jpg"];

export const needle = "feed cat";

describe("uFuzzy.search", () => {
    it("works with strings", () => {
        const uf = new ufuzzy({});
        const result = toArray(map((x) => haystack[~(~x)], uf.filter(haystack, needle)));
        assertEqual(result[0], "/feeding/the/catPic.jpg");
    });
});


"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CustomValidators = /** @class */ (function () {
    function CustomValidators() {
    }
    CustomValidators.checkPercentageSum = function (sections) {
        if (sections) {
            var sumOfPercentages_1 = 0;
            sections['controls'].forEach(function (sectionItem) {
                var percentage = sectionItem['controls'].percentage.value;
                sumOfPercentages_1 = Number(percentage) + sumOfPercentages_1;
            });
            if (sumOfPercentages_1 > 100) {
                return { "higher100": true };
            }
            if (sumOfPercentages_1 < 100) {
                return { "lower100": true };
            }
        }
        return null;
    };
    return CustomValidators;
}());
exports.CustomValidators = CustomValidators;
//# sourceMappingURL=custom-validators.js.map
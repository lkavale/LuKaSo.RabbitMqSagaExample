import { FormArray, FormGroup } from "@angular/forms";

export class CustomValidators {
  static checkPercentageSum(sections: FormArray): ValidationResult {
    if (sections) {
      let sumOfPercentages: number = 0;
      sections['controls'].forEach((sectionItem: FormGroup) => {
        let percentage = sectionItem['controls'].percentage.value;
        sumOfPercentages = Number(percentage) + sumOfPercentages;
      });

      if (sumOfPercentages > 100) {
        return { "higher100": true };
      }

      if (sumOfPercentages < 100) {
        return { "lower100": true };
      }
    }
    return null;
  }
}

export interface ValidationResult {
  [key: string]: boolean;
}

import { Component, OnInit } from '@angular/core';
import { OrderManagementService } from '../../services/order-management.service';
import { FormBuilder, FormGroup, FormControl, Validators, FormArray, AbstractControl } from '@angular/forms';
import { CustomValidators } from '../../common/custom-validators';
import { AbstractClassPart } from '@angular/compiler/src/output/output_ast';
import { ChartType, ChartOptions } from 'chart.js';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';
import { LoaderIndicatorService } from '../../components/loader-indicator/loader-indicator.service';

@Component({
  selector: 'order-management-config-form',
  templateUrl: './order-management-config-form.component.html',
  styleUrls: ['./order-management-config-form.component.css']
})
export class OrderManagementConfigFormComponent implements OnInit {
  public form: FormGroup;

  constructor(private _orderManagementService: OrderManagementService, private _formBuilder: FormBuilder, private _loaderIndicatorService: LoaderIndicatorService) {
    this.form = this._formBuilder.group({ investments: this._formBuilder.array([]) });
  }

  ngOnInit() {
    this.onReset();
  }

  get isValid(): boolean {
    return this.validateForm(this.form);
  }

  get pieChartLabels(): string[] {
    const investments = this.form.controls.investments as FormArray;

    return investments.controls.map(c => {
      const investmentCategory = (c as FormGroup).controls.investmentCategory;
      return investmentCategory.get('interestRateFrom').value + ' - ' + investmentCategory.get('interestRateTo').value + ' %';
    });
  }

  get pieChartData(): number[] {
    const investments = this.form.controls.investments as FormArray;
    return investments.controls.map(c => (c as FormGroup).controls.percentage.value);
  }

  public chartColors: any[] = [{ backgroundColor: ["#762E2A", "#8B3531", "#A13D38", "#B6443E", "#C3534E", "#CB6762", "#D37B77", "#DA908C", "#E1A4A1"] }];
  public options: any = { legend: { position: 'right' } };

  private onSubmit() {
    if (!this.validateForm(this.form)) {
      return;
    }

    this._orderManagementService
      .save(this.getFormItem())
      .subscribe(c => this.onReset());
  }

  private onReset() {
    this._orderManagementService
      .get()
      .subscribe(confs => this.addFormItems(confs));
  }

  private addFormItems(items: any[]) {
    const investmentForm = items.map(i => {
      const investmentForm = this._formBuilder.group({
        investmentCategory: this._formBuilder.group({
          interestRateFrom: [null, Validators.required],
          interestRateTo: [null, Validators.required]
        }),
        percentage: [null, Validators.compose([Validators.required, Validators.pattern("^[0-9]+([.][0-9]+)?$"), Validators.min(0), Validators.max(100)])]
      });

      investmentForm.setValue(i);
      return investmentForm;
    });

    this.form.controls.investments = this._formBuilder.array(investmentForm, CustomValidators.checkPercentageSum);
  }

  private getFormItem(): any[] {
    const investments = this.form.controls.investments as FormArray;
    return investments.controls.map(c => c.value);
  }

  private validateForm(control: AbstractControl): boolean {
    if (!control.valid)
      return false;

    if (control.hasOwnProperty('controls')) {
      const controls = control['controls'];

      if (controls instanceof AbstractControl)
        return this.validateForm(controls);
      else {
        return !Object.keys(controls)
          .filter(field => controls[field] instanceof AbstractControl)
          .some(field => !this.validateForm(controls[field] as AbstractControl));
      }
    }

    return control.valid;
  }
}

import { Component, OnInit, Input } from '@angular/core';
import { StrategyApiProviderService } from '../../services/strategy-api-provider.service';
import { Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { StrategyApiService } from '../../services/strategy-api.service';
import { ActivatedRoute } from '@angular/router';
import { LoaderIndicatorService } from '../../components/loader-indicator/loader-indicator.service';

@Component({
  selector: 'strategy-config-form',
  templateUrl: './strategy-config-form.component.html',
  styleUrls: ['./strategy-config-form.component.css']
})
export class StrategyConfigFormComponent implements OnInit {
  public strategyName: string;
  public form: FormGroup;
  private _service: StrategyApiService;

  constructor(private _strategyApiProviderService: StrategyApiProviderService, private _formBuilder: FormBuilder, private _route: ActivatedRoute, private _loaderIndicatorService: LoaderIndicatorService) {
    this.form = this._formBuilder.group({
      amountFrom: [null, Validators.compose([Validators.pattern("^[0-9]+([.][0-9]+)?$"), Validators.min(100), Validators.max(1000)])],
      amountTo: [null, Validators.compose([Validators.pattern("^[0-9]+([.][0-9]+)?$"), Validators.min(100), Validators.max(1000)])],
      interestRateFrom: [null, Validators.compose([Validators.pattern("^[0-9]+([.][0-9]+)?$"), Validators.min(1), Validators.max(10)])],
      interestRateTo: [null, Validators.compose([Validators.pattern("^[0-9]+([.][0-9]+)?$"), Validators.min(1), Validators.max(10)])]
    });
  }

  ngOnInit() {
    this._route
      .data
      .subscribe(data => {
        this._service = this._strategyApiProviderService.getService(data.strategyAddress);
        this.strategyName = data.strategyName
      });

    this.onReset();
  }

  get isValid(): boolean {
    if (!this.form.valid) {
      this.validateAllFormFields(this.form);
      return false;
    }
    return true;
  }

  private onSubmit() {
    if (!this.isValid) {
      return;
    }

    this._service
      .save(this.form.value)
      .subscribe(c => this.onReset());
  }

  private onReset() {
    this._service
      .get()
      .subscribe(conf => this.form.setValue(conf));
  }

  private validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);

      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }
}

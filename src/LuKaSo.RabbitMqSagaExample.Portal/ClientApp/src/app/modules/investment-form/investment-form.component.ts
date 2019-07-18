import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { InvestmentService } from '../../services/investment.service';
import { PortfolioService } from '../../services/portfolio.service';
import { LoaderIndicatorService } from '../../components/loader-indicator/loader-indicator.service';

@Component({
  selector: 'investment-form',
  templateUrl: './investment-form.component.html',
  styleUrls: ['./investment-form.component.css']
})
export class InvestmentFormComponent implements OnInit {
  public form: FormGroup;
  public investments: any[];
  private hasData: boolean;

  constructor(private _investmentService: InvestmentService, private _portfolioService: PortfolioService, private _formBuilder: FormBuilder, private _loaderIndicatorService: LoaderIndicatorService) {
    this.form = this._formBuilder.group({
      amount: [null, Validators.compose([Validators.required, Validators.pattern("^[0-9]+([.][0-9]+)?$"), Validators.min(100), Validators.max(1000)])],
      interestRate: [null, Validators.compose([Validators.required, Validators.pattern("^[0-9]+([.][0-9]+)?$"), Validators.min(1), Validators.max(10)])]
    });

    this.hasData = false;
  }

  ngOnInit() {
    this.getInvestments();
  }

  public onSubmit() {
    this._investmentService.addInvestment(this.form.value).subscribe();

    this.onReset();
  }

  public onReset() {
    this.form.reset();
  }

  public getInvestments() {
    this._portfolioService
      .getInvestments()
      .subscribe(data => {
        this.investments = data

        if (this.investments.length > 0)
          this.hasData = true;
      });
  }
}

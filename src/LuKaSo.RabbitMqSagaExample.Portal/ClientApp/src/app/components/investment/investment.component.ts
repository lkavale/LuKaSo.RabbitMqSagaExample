import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'investment',
  templateUrl: './investment.component.html',
  styleUrls: ['./investment.component.css']
})
export class InvestmentComponent implements OnInit {
  @Input() form: FormGroup;
  @Output() reset = new EventEmitter<void>();

  get validationMessages(): any {
    return {
      'amount': [
        { type: 'required', message: 'Amount is required' },
        { type: 'pattern', message: 'Amount must be a number' },
        { type: 'min', message: 'Amount must be bigger then 100' },
        { type: 'max', message: 'Amount must be smaller then 1000' }
      ],
      'interestRate': [
        { type: 'required', message: 'Interest rate is required' },
        { type: 'pattern', message: 'Amount must be a number' },
        { type: 'min', message: 'Interest rate must be bigger then 1' },
        { type: 'max', message: 'Interest rate must be smaller then 10' }
      ]
    };
  }

  constructor() { }

  ngOnInit() {
  }

  public onReset() {
    this.reset.emit();
  }
}

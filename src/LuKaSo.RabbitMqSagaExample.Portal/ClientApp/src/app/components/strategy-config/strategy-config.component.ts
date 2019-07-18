import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'strategy-config',
  templateUrl: './strategy-config.component.html',
  styleUrls: ['./strategy-config.component.css']
})
export class StrategyConfigComponent implements OnInit {
  @Input() form: FormGroup;
  @Output() reset = new EventEmitter<void>();

  get validationMessages(): any {
    return {
      'amountFrom': [
        { type: 'pattern', message: 'Amount must be a number' },
        { type: 'min', message: 'Amount from must be higher then 100' },
        { type: 'max', message: 'Amount from must be lower then 1000' }
      ],
      'amountTo': [
        { type: 'pattern', message: 'Amount must be a number' },
        { type: 'min', message: 'Amount to must be higher then 100' },
        { type: 'max', message: 'Amount to must be lower then 1000' }
      ],
      'interestRateFrom': [
        { type: 'pattern', message: 'Amount must be a number' },
        { type: 'min', message: 'Interest rate from must be higher then 1' },
        { type: 'max', message: 'Interest rate from must be lower then 10' }
      ],
      'interestRateTo': [
        { type: 'pattern', message: 'Amount must be a number' },
        { type: 'min', message: 'Interest rate to must be higher then 1' },
        { type: 'max', message: 'Interest rate to must be lower then 10' }
      ]
    };
  }

  constructor() { }

  ngOnInit() {
  }

  private onReset() {
    this.reset.emit();
  }
}

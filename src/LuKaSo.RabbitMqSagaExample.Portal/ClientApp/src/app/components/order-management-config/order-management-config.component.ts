import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { MatTableDataSource } from '@angular/material';

@Component({
  selector: 'order-management-config',
  templateUrl: './order-management-config.component.html',
  styleUrls: ['./order-management-config.component.css']
})
export class OrderManagementConfigComponent implements OnInit {
  @Input() form: FormGroup;
  @Output() reset = new EventEmitter<void>();

  displayedColumns = ['interestRateRange', 'percentage'];

  get validationMessages(): any {
    return {
      'percentage': [
        { type: 'required', message: 'Percentage is required' },
        { type: 'min', message: 'Percentage must be higher then 0' },
        { type: 'max', message: 'Percentage must be lower then 100' }
      ],
      'investments': [
        { type: 'higher100', message: 'Sum of percentage must be equal to 100' },
        { type: 'lower100', message: 'Sum of percentage must be equal to 100' }
      ]
    };
  }

  get formArray(): FormArray {
    return this.form.get('investments') as FormArray;
  }

  get dataSource(): MatTableDataSource<any> {
    return new MatTableDataSource((this.form.get('investments') as FormArray).controls);
  }

  constructor() { }

  ngOnInit() {
  }

  private onReset() {
    this.reset.emit();
  }
}

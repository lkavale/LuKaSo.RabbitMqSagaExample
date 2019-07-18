import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'investment-list',
  templateUrl: './investment-list.component.html',
  styleUrls: ['./investment-list.component.css']
})
export class InvestmentListComponent implements OnInit {
  @Input() investments: any[];

  displayedColumns: string[] = ['id', 'investmentTime', 'amount', 'interestRate'];

  constructor() { }

  ngOnInit() {
  }
}

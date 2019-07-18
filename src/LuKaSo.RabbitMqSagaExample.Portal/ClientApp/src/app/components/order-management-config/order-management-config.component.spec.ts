import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderManagementConfigComponent } from './order-management-config.component';

describe('OrderManagementConfigComponent', () => {
  let component: OrderManagementConfigComponent;
  let fixture: ComponentFixture<OrderManagementConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderManagementConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderManagementConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

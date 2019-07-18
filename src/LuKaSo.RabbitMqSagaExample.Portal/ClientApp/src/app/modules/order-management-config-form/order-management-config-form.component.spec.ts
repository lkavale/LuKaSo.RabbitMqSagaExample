import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderManagementConfigFormComponent } from './order-management-config-form.component';

describe('OrderManagementConfigFormComponent', () => {
  let component: OrderManagementConfigFormComponent;
  let fixture: ComponentFixture<OrderManagementConfigFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OrderManagementConfigFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderManagementConfigFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StrategyConfigFormComponent } from './strategy-config-form.component';

describe('StrategyConfigFormComponent', () => {
  let component: StrategyConfigFormComponent;
  let fixture: ComponentFixture<StrategyConfigFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StrategyConfigFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StrategyConfigFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

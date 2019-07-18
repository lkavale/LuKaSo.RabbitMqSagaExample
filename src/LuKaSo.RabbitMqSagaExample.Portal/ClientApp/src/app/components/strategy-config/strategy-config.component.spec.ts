import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StrategyConfigComponent } from './strategy-config.component';

describe('StrategyConfigComponent', () => {
  let component: StrategyConfigComponent;
  let fixture: ComponentFixture<StrategyConfigComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StrategyConfigComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StrategyConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoansMakeComponent } from './loans-make.component';

describe('LoansMakeComponent', () => {
  let component: LoansMakeComponent;
  let fixture: ComponentFixture<LoansMakeComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LoansMakeComponent]
    });
    fixture = TestBed.createComponent(LoansMakeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

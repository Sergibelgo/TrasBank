import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoanPreviewComponent } from './loan-preview.component';

describe('LoanPreviewComponent', () => {
  let component: LoanPreviewComponent;
  let fixture: ComponentFixture<LoanPreviewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LoanPreviewComponent]
    });
    fixture = TestBed.createComponent(LoanPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

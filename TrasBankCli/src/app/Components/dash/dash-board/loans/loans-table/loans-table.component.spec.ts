import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoansTableComponent } from './loans-table.component';

describe('LoansTableComponent', () => {
  let component: LoansTableComponent;
  let fixture: ComponentFixture<LoansTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LoansTableComponent]
    });
    fixture = TestBed.createComponent(LoansTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionsInfoWorkerComponent } from './transactions-info-worker.component';

describe('TransactionsInfoWorkerComponent', () => {
  let component: TransactionsInfoWorkerComponent;
  let fixture: ComponentFixture<TransactionsInfoWorkerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [TransactionsInfoWorkerComponent]
    });
    fixture = TestBed.createComponent(TransactionsInfoWorkerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

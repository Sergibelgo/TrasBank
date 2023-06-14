import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalEditAccountWorkerComponent } from './modal-edit-account-worker.component';

describe('ModalEditAccountWorkerComponent', () => {
  let component: ModalEditAccountWorkerComponent;
  let fixture: ComponentFixture<ModalEditAccountWorkerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalEditAccountWorkerComponent]
    });
    fixture = TestBed.createComponent(ModalEditAccountWorkerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { Component, OnInit } from '@angular/core';
import { ModalService } from './services/modal.service';
import { ModalContent } from './shared/modal/modal-content.modal';
import { UserFormComponent } from './user/user-form/user-form.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  isModalOpen: boolean = false;
  modalContent: ModalContent = {
    component: UserFormComponent,
    data: { isEdit: true },
  };

  constructor(private modalService: ModalService) {}

  ngOnInit() {
    this.modalService.modalIsVisibleChanged.subscribe(
      (isModalOpen: boolean) => (this.isModalOpen = isModalOpen)
    );

    this.modalService.modalContentChanged.subscribe(
      (modalContent: ModalContent) => {
        this.modalContent = modalContent;
      }
    );
  }
}

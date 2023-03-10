import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { ModalContent } from '../shared/modal/modal-content.modal';

@Injectable({
  providedIn: 'root',
})
export class ModalService {
  modalIsVisibleChanged = new Subject<boolean>();
  modalContentChanged = new Subject<ModalContent>();

  private modalIsVisible: boolean;
  private modalContent: ModalContent;

  getModalIsVisible() {
    return this.modalIsVisible;
  }

  setModalIsVisible(isVisible: boolean) {
    this.modalIsVisible = isVisible;
    this.modalIsVisibleChanged.next(this.modalIsVisible);
  }

  getModalContent() {
    return this.modalContent;
  }

  setModalContent(content: ModalContent) {
    this.modalContent = content;
    this.modalContentChanged.next(this.modalContent);
  }
}

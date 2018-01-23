import { Patient } from "./Patient"

export class TakeSlot {
    public facilityId: string
    public start: Date
    public end: Date
    public comments: string
    public patient: Patient
}
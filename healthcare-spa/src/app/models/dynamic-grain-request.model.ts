export interface DynamicGrainRequest {
  sessionRef: string;
  grainType: string;
  grainKey?: string;
}

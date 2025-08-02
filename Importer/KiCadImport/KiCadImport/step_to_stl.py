import sys
import os
from OCC.Core.STEPControl import STEPControl_Reader
from OCC.Core.IFSelect import IFSelect_RetDone
from OCC.Extend.DataExchange import write_stl_file

def convert_step_to_stl(input_path, output_path):
    reader = STEPControl_Reader()
    status = reader.ReadFile(input_path)
    if status != IFSelect_RetDone:
        print("Failed to read STEP file")
        return 1
    reader.TransferRoot()
    shape = reader.Shape()
    write_stl_file(shape, output_path)
    print("OK")
    return 0

if __name__ == "__main__":
    if len(sys.argv) != 3:
        print("Usage: step_to_stl.py input.stp output.stl")
        sys.exit(1)

    in_path = os.path.abspath(sys.argv[1])
    out_path = os.path.abspath(sys.argv[2])
    sys.exit(convert_step_to_stl(in_path, out_path))

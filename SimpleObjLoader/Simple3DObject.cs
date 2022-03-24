using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleObjLoader
{
    public class Simple3DObject
    {
        public float[] Vertices;
        public float[] Textures;
        public float[] Normals;
        public uint[] VerticesIndices;
        public uint[] TexturesIndices;
        public uint[] NormalsIndices;
        public string Name;
        public Simple3DObject(string filePath)
        {

            using (StreamReader sr = new StreamReader(filePath))
            {
                List<float> vertices = new List<float>();
                List<float> normals = new List<float>();
                List<float> textures = new List<float>();

                List<uint> verticesIndices = new List<uint>();
                List<uint> normalsIndices = new List<uint>();
                List<uint> texturesIndices = new List<uint>();
                while(!sr.EndOfStream)
                {
                    string? line = sr.ReadLine();
                    if (line != null)
                    {
                        var elements = line.Split(' ', '/');
                        switch(elements[0])
                        {
                            case "o":
                                Name = elements[1];
                                break;
                            case "v":
                                vertices.Add(float.Parse(elements[1], CultureInfo.InvariantCulture.NumberFormat));
                                vertices.Add(float.Parse(elements[2], CultureInfo.InvariantCulture.NumberFormat));
                                vertices.Add(float.Parse(elements[3], CultureInfo.InvariantCulture.NumberFormat));
                                break;
                            case "vn":
                                normals.Add(float.Parse(elements[1], CultureInfo.InvariantCulture.NumberFormat));
                                normals.Add(float.Parse(elements[2], CultureInfo.InvariantCulture.NumberFormat));
                                normals.Add(float.Parse(elements[3], CultureInfo.InvariantCulture.NumberFormat));
                                break;
                            case "vt":
                                textures.Add(float.Parse(elements[1], CultureInfo.InvariantCulture.NumberFormat));
                                textures.Add(float.Parse(elements[2], CultureInfo.InvariantCulture.NumberFormat));
                                break;
                            case "f":
                                for(int j=0; j<3; j++)
                                {
                                    //uint vertice = uint.Parse(elements[j * 3]);
                                    //uint normal = uint.Parse(elements[j * 3 + 1]);
                                    //uint texture = uint.Parse(elements[j * 3 + 2]);

                                    verticesIndices.Add(uint.Parse(elements[j * 3 + 1]));
                                    texturesIndices.Add(uint.Parse(elements[j * 3 + 2]));
                                    normalsIndices.Add(uint.Parse(elements[j * 3 + 3]));
                                }
                                break;
                        }
                    }
                }
                Vertices = vertices.ToArray();
                Textures = textures.ToArray();
                Normals = normals.ToArray();
                VerticesIndices = verticesIndices.ToArray();
                TexturesIndices = texturesIndices.ToArray();
                NormalsIndices = normalsIndices.ToArray();
            }
        }
    }
}

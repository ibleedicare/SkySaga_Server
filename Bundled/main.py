import argparse
import json

def map_parameters_to_packet_size(parameters):
    """
    Calculate the packet size for a specific entity based on the parameters
    :param the parameters from the entity
    """
    packet_size = 0
    for parameter_name, option in parameters.items():
        if "value" in option and hasattr(option['value'], "__len__"):
            packet_size += len(option["value"])
        else:
            packet_size += 1
        try:
            print(f"Sync Index {option['syncindex']} is parameter {parameter_name}")
        except:
            print(f"No sync index for parameter {parameter_name}")
    return packet_size

        
def find_entity_index(data, name):
    """
    Find the index of an entity in the JSON list where 'Name' matches the given name.
    :param data: JSON data containing a list of entities.
    :param name: The name to search for.
    :return: Index of the entity or -1 if not found.
    """
    return next((i for i, entity in enumerate(data.get("Entities", [])) if entity.get("Name") == name), -1)

def main():
    # Set up argument parser
    parser = argparse.ArgumentParser(description="Lookup the index of an entity by name in JSON data.")
    parser.add_argument("--name", required=False, help="The name of the entity to search for.")
    parser.add_argument("--file", required=True, help="Path to the JSON file.")
    parser.add_argument("--dump-entity", required=False, type=int, help="dump the entity")
    parser.add_argument("--get-entities", required=False, action="store_true")
    parser.add_argument("--debug", required=False, action="store_true")

    args = parser.parse_args()

    # Read the JSON file
    try:
        with open(args.file, "r") as file:
            data = json.load(file)
    except Exception as e:
        print(f"Error reading JSON file: {e}")
        return

    if args.file:
        if args.name:
            # Find the index
            index = find_entity_index(data, args.name)
            if index != -1:
                print(f"Entity with name '{args.name}' found at index {index}.")
            else:
                print(f"Entity with name '{args.name}' not found.")
        if args.dump_entity:
            if args.debug:
                print(json.dumps(data["Entities"][args.dump_entity], indent=4))
            packet_size = map_parameters_to_packet_size(data["Entities"][args.dump_entity]['parameters'])
            print(f"{data['Entities'][args.dump_entity]['Name']} Packet size {packet_size}")
        if args.get_entities: 
            for entity in data["Entities"]:
                print(f"{entity['Name']}")

if __name__ == "__main__":
    main()


# SeqIngestor

## Usage

1. **Download the Repository**
   - Clone or download the repository to your local machine.

2. **Run Docker**
   ```bash
   docker-compose up // runs api, seq and seqcli
   ```
   
3. **Access Swagger UI**
   - Open your browser and navigate to [https://localhost:5001/swagger/index.html](https://localhost:5001/swagger/index.html).

4. **Invoke the Ingestion Endpoint**
   - Use the Swagger UI to invoke the endpoint `api/seq/ingest`.

5. **Access Seq**
   - Open [http://localhost:5340/#](http://localhost:5340/#) to view and trace your logs.

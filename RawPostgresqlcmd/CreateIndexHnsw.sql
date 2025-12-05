CREATE INDEX idx_chunks_embedding_hnsw
ON "Chunks"
USING hnsw ("Embedding" vector_l2_ops)
WITH (m=16, ef_construction=200);   